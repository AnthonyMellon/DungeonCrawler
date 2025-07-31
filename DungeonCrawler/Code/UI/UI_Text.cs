using DungeonCrawler.Code.DrawManagement;
using DungeonCrawler.Code.Scenes;
using DungeonCrawler.Code.UI.Utils;
using DungeonCrawler.Code.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonCrawler.Code.UI
{
    internal class UI_Text : UIComponent
    {
        public UI_Text(
            string text,
            SpriteFont font,
            Color color,
            GameConstants.GameLayers layer,
            AnchorPoints anchorPoints,
            Size size,
            Offset offset,
            DynamicRectangle.FitTypes fitType,
            DynamicRectangle.GrowFroms growFrom,
            Scene scene,
            bool enabled = true) :
            base(anchorPoints, size, offset, fitType, growFrom, scene, enabled)
        {
            BuildText(text, font, color, layer);
        }

        public UI_Text(
            string text,
            Color color,
            GameConstants.GameLayers layer,
            AnchorPoints anchorPoints,
            Size size,
            Offset offset,
            DynamicRectangle.FitTypes fitType,
            DynamicRectangle.GrowFroms growFrom,
            Scene scene,
            bool enabled = true) :
            base(anchorPoints, size, offset, fitType, growFrom, scene, enabled)
        {
            BuildText(text, DefaultContent.DefaultFont, color, layer);
        }

        private DrawableText _text;

        private void BuildText(string text, SpriteFont font, Color color, GameConstants.GameLayers layer)
        {
            _text = new DrawableText(
                text,
                Rectangle.ScreenLocation,
                color,
                font,
                layer,
                Scene);
        }
    }
}
