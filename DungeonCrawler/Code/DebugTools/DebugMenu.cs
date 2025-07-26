/*#if DEVELOPMENT
using DungeonCrawler.Code.Input;
using DungeonCrawler.Code.UI;
using DungeonCrawler.Code.Utils;
using DungeonCrawler.Code.Utils.Drawables;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DungeonCrawler.Code.DebugTools
{
    internal class DebugMenu : UIComponent
    {        
        public DebugMenu(
            Vector4 anchorPoints,
            Point4 padding,
            Point offset,
            FitTypes fitType = FitTypes.Screen,
            bool enabled = true) :
            base(anchorPoints, padding, offset, fitType, enabled)
        {
            InputProvider.RegisterActionToKeyTap(Keys.F3, () => IsEnabled = !IsEnabled);

            _backgroundTexture = new DrawableTexture(
                DefaultContent.DefaultRectangle,
                new Rectangle(0, 0, DrawRectangle.Width, DrawRectangle.Height),
                new Color(1, 1, 1, 0.9f),
                GameConstants.GameLayers.Top);

            _renderTarget = new RenderTarget2D(GameValues.GraphicsDevice, DrawRectangle.Width, DrawRectangle.Height);
        }

        private RenderTarget2D _renderTarget;
        private DrawableTexture _backgroundTexture;
        private DrawableTexture _titleTexture;

        protected override void PreDraw(SpriteBatch spriteBatch)
        {
            GameValues.GraphicsDevice.SetRenderTarget(_renderTarget);
            GameValues.GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(blendState: BlendState.NonPremultiplied);
            _backgroundTexture.Draw(spriteBatch);
            spriteBatch.End();

            GameValues.GraphicsDevice.SetRenderTarget(null);
            GameValues.GraphicsDevice.Clear(Color.CornflowerBlue);
        }

        protected override void Draw(GameTime gametime, Camera camera)
        {
            camera.DrawTexture(new DrawableTexture(
                _renderTarget,
                DrawRectangle,
                Color.White,
                GameConstants.GameLayers.Top
                ));
        }
    }
}

#endif
*/