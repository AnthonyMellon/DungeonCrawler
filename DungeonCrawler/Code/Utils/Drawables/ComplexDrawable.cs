using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Windows.Forms;

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
            drawable.OnChange += BuildTexture;
        }

        public void RemoveDrawable(Drawable drawable)
        {
            drawable.OnChange -= BuildTexture;
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            if (_renderTarget == null) return;

            spritebatch.Draw(
                _renderTarget,
                Rectangle,
                Color.White);
        }

        private RenderTarget2D _renderTarget;
        private List<Drawable> _drawables = new List<Drawable>();
        private GraphicsDevice _grahpicsDevice;
        private SpriteBatch _spriteBatch;

        public ComplexDrawable(
            GraphicsDevice graphicsDevice,
            SpriteBatch spritebatch,
            DrawManager.DrawTargets drawTarget = DrawManager.DrawTargets.None,
            bool visible = true) :
            base(drawTarget, visible)
        {
            _grahpicsDevice = graphicsDevice;
            _spriteBatch = spritebatch;
            BuildTexture();
        }

        protected void BuildTexture()
        {
            // TODO: FIX THIS
            _renderTarget = new RenderTarget2D(GameValues.GraphicsDevice, 100, 100);

            _grahpicsDevice.SetRenderTarget(_renderTarget);
            _grahpicsDevice.Clear(GameConstants.DEFAULT_COLOR);

            _spriteBatch.Begin();
            for (int i = 0; i < _drawables.Count; i++)
            {
                _drawables[i].Draw(_spriteBatch);
            }
            _spriteBatch.End();

            _grahpicsDevice.SetRenderTarget(null);
            _grahpicsDevice.Clear(GameConstants.DEFAULT_COLOR);
        }
    }
}
