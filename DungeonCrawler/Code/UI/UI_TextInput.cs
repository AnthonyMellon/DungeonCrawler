using DungeonCrawler.Code.DrawManagement;
using DungeonCrawler.Code.UI.Utils;
using DungeonCrawler.Code.Utils;
using DungeonCrawler.Code.Utils.Drawables;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace DungeonCrawler.Code.UI
{
    internal class UI_TextInput : UIComponent
    {
        public UI_TextInput(
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
            BuildTextInput(layer);
        }

        private void BuildTextInput(GameConstants.GameLayers layer)
        {
            DrawableTexture backgroundTexture = new DrawableTexture(
                DefaultContent.DefaultRectangle,
                Point.Zero,
                Color.Blue,
                GameConstants.GameLayers.Bottom);
            _backgroundTexture = backgroundTexture;

            DrawableTexture inputAreaTexture = new DrawableTexture(
                DefaultContent.DefaultRectangle,
                Point.Zero,
                Color.LightBlue,
                GameConstants.GameLayers.Bottom);
            _inputAreaTexture = inputAreaTexture;

            DrawableText text = new DrawableText(
                "Test Text",
                Point.Zero,
                Color.Black,
                GameConstants.GameLayers.Bottom);
            text.CenterTextToRectangle(inputAreaTexture.Rectangle.Rectangle);
            _text = text;

            ComplexDrawable drawTexture = new ComplexDrawable(GameValues.GraphicsDevice, layer, DrawTarget);
            drawTexture.AddDrawables(new List<Drawable>
            {
                backgroundTexture,
                inputAreaTexture,
                text
            });
            DrawTexture = drawTexture;
        }

        private DrawableTexture _backgroundTexture;
        private DrawableTexture _inputAreaTexture;
        private DrawableText _text;
        private ComplexDrawable _finalTexture;

        private string _emptyText = "Type Here...";
    }
}
