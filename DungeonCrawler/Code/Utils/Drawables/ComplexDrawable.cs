using DungeonCrawler.Code.DrawManagement;
using DungeonCrawler.Code.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

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
                Rectangle.Rectangle,
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
            Scene scene,
            bool visible = true) :
            base(layer, scene, visible)
        {
            _grahpicsDevice = graphicsDevice;
            DrawManager.RegisterComplexDrawable(this);
        }

        private RenderTarget2D _renderTarget;
        private List<Drawable> _drawables = new List<Drawable>();
        private GraphicsDevice _grahpicsDevice;
        private bool _hasChanged = true;

        private void MarkForTextureRebuild()
        {
            _hasChanged = true;
        }

        protected void BuildTexture()
        {
            if (Rectangle.ScreenSize.X == 0 || Rectangle.ScreenSize.Y == 0) return;

            _renderTarget = new RenderTarget2D(GameValues.GraphicsDevice, Rectangle.ScreenSize.X, Rectangle.ScreenSize.Y);
            SpriteBatch spriteBatch = new SpriteBatch(_grahpicsDevice);

            _grahpicsDevice.SetRenderTarget(_renderTarget);
            _grahpicsDevice.Clear(Color.Transparent);

            spriteBatch.Begin();
            for (int i = 0; i < _drawables.Count; i++)
            {
                _drawables[i].Draw(spriteBatch, _drawables[i].Rectangle.Rectangle);
            }
            spriteBatch.End();
            spriteBatch.Dispose();

            _grahpicsDevice.SetRenderTarget(null);
            _grahpicsDevice.Clear(GameConstants.DEFAULT_COLOR);

            _hasChanged = false;
        }
    }
}
