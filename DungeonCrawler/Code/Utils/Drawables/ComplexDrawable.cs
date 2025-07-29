using DungeonCrawler.Code.DrawManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics;

namespace DungeonCrawler.Code.Utils.Drawables
{
    internal class ComplexDrawable : Drawable
    {
        public void AddDrawables(List<Drawable> drawables)
        {
            for (int i = 0; i < drawables.Count; i++)
            {
                AddDrawable(drawables[i]);
            }
        }

        public void AddDrawable(Drawable drawable)
        {
            drawable.OnChange += MarkForTextureRebuild;
            _drawables.Add(drawable);
        }

        public void RemoveDrawable(Drawable drawable)
        {
            drawable.OnChange -= MarkForTextureRebuild;
            _drawables.Remove(drawable);
        }

        public void PreDraw()
        {
            if (_hasChanged) BuildTexture();
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            if (_renderTarget == null) return;

            spritebatch.Draw(
                _renderTarget,
                Rectangle,
                Color.White);
        }

        public override void Draw(SpriteBatch spritebatch, Rectangle destinationRectangle)
        {
            if (_renderTarget == null) return;

            spritebatch.Draw(
                _renderTarget,
                destinationRectangle,
                Color.White);
        }

        public ComplexDrawable(
            GraphicsDevice graphicsDevice,
            GameConstants.GameLayers layer,
            DrawManager.DrawTargets drawTarget = DrawManager.DrawTargets.None,
            bool visible = true) :
            base(layer, drawTarget, visible)
        {
            _grahpicsDevice = graphicsDevice;
            RegisterToPreDraw(Visible);
            OnVisabilitySet += RegisterToPreDraw;
        }

        private RenderTarget2D _renderTarget;
        private List<Drawable> _drawables = new List<Drawable>();
        private GraphicsDevice _grahpicsDevice;
        private bool _hasChanged = true;

        private void RegisterToPreDraw(bool visable)
        {
            if(visable) DrawManager.RegisterComplexDrawable(this);
            else DrawManager.DeregisterComplexDrawable(this);
        }

        private void MarkForTextureRebuild()
        {
            _hasChanged = true;
        }

        protected void BuildTexture()
        {
            if (Rectangle.Width == 0 || Rectangle.Height == 0) return;

            _renderTarget = new RenderTarget2D(GameValues.GraphicsDevice, Rectangle.Width, Rectangle.Height);
            SpriteBatch spriteBatch = new SpriteBatch(_grahpicsDevice);

            _grahpicsDevice.SetRenderTarget(_renderTarget);
            _grahpicsDevice.Clear(Color.Transparent);

            spriteBatch.Begin();
            for (int i = 0; i < _drawables.Count; i++)
            {
                _drawables[i].Draw(spriteBatch, _renderTarget.Bounds);
            }
            spriteBatch.End();
            spriteBatch.Dispose();

            _grahpicsDevice.SetRenderTarget(null);
            _grahpicsDevice.Clear(GameConstants.DEFAULT_COLOR);

            _hasChanged = false;
        }
    }
}
