using DungeonCrawler.Code.DrawManagement;
using DungeonCrawler.Code.UI.Utils;
using DungeonCrawler.Code.Utils;
using DungeonCrawler.Code.Utils.Drawables;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonCrawler.Code.UI
{
    internal class UI_Text : UIComponent
    {
        /*        public UI_Text(
                    string text,
                    SpriteFont font,
                    Color color,
                    AnchorPoints anchorPoints,
                    Size padding,
                    Offset offset,
                    GameConstants.GameLayers layer,
                    DrawManager.DrawTargets drawTarget,
                    FitTypes fitType = FitTypes.Parent,
                    bool enabled = true) :
                    base(anchorPoints, padding, offset, drawTarget, fitType, enabled)
                {
                    BuildText(text, font, color, layer);
                }*/

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
            DrawManager.DrawTargets drawTarget,
            bool enabled = true) :
            base(anchorPoints, size, offset, fitType, growFrom, drawTarget, enabled)
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
            DrawManager.DrawTargets drawTarget,
            bool enabled = true) :
            base(anchorPoints, size, offset, fitType, growFrom, drawTarget, enabled)
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
                DrawTarget);
        }
    }
}
