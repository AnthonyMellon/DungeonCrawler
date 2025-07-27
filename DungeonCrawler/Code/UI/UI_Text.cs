using DungeonCrawler.Code.UI.Utils;
using DungeonCrawler.Code.Utils;
using DungeonCrawler.Code.Utils.Drawables;
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
            AnchorPoints anchorPoints,
            Padding padding,
            Point offset,
            GameConstants.GameLayers layer,
            DrawManager.DrawTargets drawTarget,
            FitTypes fitType = FitTypes.Parent,
            bool enabled = true) :
            base(anchorPoints, padding, offset, drawTarget, fitType, enabled)
        {
            BuildText(text, font, color, layer);
        }

        public UI_Text(
            string text,
            Color color,
            AnchorPoints anchorPoints,
            Padding padding,
            Point offset,
            GameConstants.GameLayers layer,
            DrawManager.DrawTargets drawTarget,
            FitTypes fitType = FitTypes.Parent,
            bool enabled = true) :
            base(anchorPoints, padding, offset, drawTarget, fitType, enabled)
        {
            BuildText(text, DefaultContent.DefaultFont, color, layer);
        }

        private DrawableText _text;

        private void BuildText(string text, SpriteFont font, Color color, GameConstants.GameLayers layer)
        {
            _text = new DrawableText(
                text,
                DrawRectangle.Location,
                color,
                font,
                layer,
                DrawTarget);
        }
    }
}
