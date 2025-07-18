using DungeonCrawler.Code.Utils;
using DungeonCrawler.Code.Utils.Drawables;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonCrawler.Code.UI
{
    internal class UI_Panel : UIComponent
    {
        public UI_Panel(
            Vector4 anchorPoints,
            Point4 padding,
            Point offset,
            Texture2D texture,
            Color color) :
            base(anchorPoints, padding, offset)
        {
            _backgroundSprite = new DrawableTexture(
                texture == null ? DefaultContent.DefaultRectangle : texture,
                ScreenRectangle,
                color,
                GameConstants.GameLayers.UI);
        }

        public UI_Panel(
            Vector4 anchorPoints,
            Point4 padding,
            Point offset,
            Texture2D texture) :
            base(anchorPoints, padding, offset)
        {
            _backgroundSprite = new DrawableTexture(
                texture == null ? DefaultContent.DefaultRectangle : texture,
                ScreenRectangle,
                Color.White,
                GameConstants.GameLayers.UI);
        }

        public UI_Panel(
            Vector4 anchorPoints,
            Point4 padding,
            Point offset) :
            base(anchorPoints, padding, offset)
        {
            _backgroundSprite = null;
        }

        private DrawableTexture _backgroundSprite;

        protected override void Draw(GameTime gametime, Camera camera)
        {
            if (_backgroundSprite != null)
            {
                camera.DrawTexture(_backgroundSprite);
            }
        }
    }
}
