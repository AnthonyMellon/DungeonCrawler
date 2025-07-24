using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace DungeonCrawler.Code.Utils.Drawables
{
    internal class ComplexDrawable : Drawable
    {
        private RenderTarget2D _renderTarget;
        private List<Drawable> _drawables = new List<Drawable>();

        public override void Draw(SpriteBatch spritebatch, GameTime gameTime)
        {
            for(int i = 0;  i < _drawables.Count; i++)
            {
                _drawables[i].Draw(spritebatch, gameTime);
            }
        }

        protected ComplexDrawable(
            GraphicsDevice graphicsDevice,
            SpriteBatch spritebatch,
            GameTime gametime,
            DrawManager.DrawTargets drawTarget,
            bool visible = true) :
            base(drawTarget, visible)
        {
            BuildTexture(graphicsDevice, spritebatch, gametime);
        }

        protected void BuildTexture(GraphicsDevice graphicsDevice, SpriteBatch spritebatch, GameTime gametime)
        {
            RenderTarget2D renderTarget = _renderTarget;

            graphicsDevice.SetRenderTarget(renderTarget);
            graphicsDevice.Clear(GameConstants.DEFAULT_COLOR);

            spritebatch.Begin();
            for (int i = 0; i < _drawables.Count; i++)
            {
                _drawables[i].Draw(spritebatch, gametime);
            }
            spritebatch.End();

            graphicsDevice.SetRenderTarget(null);
            graphicsDevice.Clear(GameConstants.DEFAULT_COLOR);
        }
    }
}
