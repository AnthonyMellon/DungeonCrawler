using DungeonCrawler.Code.Utils;
using DungeonCrawler.Code.Utils.Drawables;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace DungeonCrawler.Code.UI
{
    internal class UI_Panel : UIComponent
    {
        public UI_Panel(
            Vector4 anchorPoints,
            Point4 padding,
            Point offset,
            Texture2D texture,
            Color color,
            FitTypes fitType = FitTypes.Parent) :
            base(anchorPoints, padding, offset, fitType)
        {
            _backgroundSprite = new DrawableTexture(
                texture == null ? DefaultContent.DefaultRectangle : texture,
                DrawRectangle,
                color,
                GameConstants.GameLayers.UI);
            OnDrawRectangleUpdated += UpdateDestinationRectangle;
        }

        public UI_Panel(
            Vector4 anchorPoints,
            Point4 padding,
            Point offset,
            FitTypes fitType = FitTypes.Parent) :
            base(anchorPoints, padding, offset, fitType)
        {
            _backgroundSprite = null;
            OnDrawRectangleUpdated += UpdateDestinationRectangle;
        }

        private DrawableTexture _backgroundSprite;

        private void UpdateDestinationRectangle()
        {
            if (_backgroundSprite == null) return;

            _backgroundSprite.DestinationRectangle = DrawRectangle;
        }

        protected override void Draw(GameTime gametime, Camera camera)
        {
            if (_backgroundSprite != null)
            {
                camera.DrawTexture(_backgroundSprite);
            }
        }
    }
}
