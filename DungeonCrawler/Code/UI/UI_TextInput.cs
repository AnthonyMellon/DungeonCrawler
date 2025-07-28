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
            AnchorPoints anchorPoints,
            Padding padding, Offset offset,
            DrawManager.DrawTargets drawTarget = DrawManager.DrawTargets.None,
            FitTypes fitType = FitTypes.Parent,
            bool enabled = true) :
            base(anchorPoints, padding, offset, drawTarget, fitType, enabled)
        {
            OnDrawRectangleUpdated += UpdateDrawRectangle;
            BuildTextInput();
        }

        private void UpdateDrawRectangle()
        {
            if (_finalTexture == null) return;

            _finalTexture.Position = DrawRectangle.Location;
            _finalTexture.Size = DrawRectangle.Size;
        }

        private void BuildTextInput()
        {
            DrawableTexture backgroundTexture = new DrawableTexture(
                DefaultContent.DefaultRectangle,
                Point.Zero,
                Color.White,
                GameConstants.GameLayers.Bottom);
            _backgroundTexture = backgroundTexture;

            DrawableTexture inputAreaTexture = new DrawableTexture(
                DefaultContent.DefaultRectangle,
                Point.Zero,
                Color.AliceBlue,
                GameConstants.GameLayers.Bottom);
            _inputAreaTexture = inputAreaTexture;

            DrawableText text = new DrawableText(
                "Test Text",
                Point.Zero,
                Color.Black);
            text.CenterTextToRectangle(inputAreaTexture.Rectangle);
            _text = text;

            _finalTexture = new ComplexDrawable(GameValues.GraphicsDevice, DrawTarget);
            _finalTexture.AddDrawables(new List<Drawable>
            {
                backgroundTexture,
                inputAreaTexture,
                text
            });
        }

        private DrawableTexture _backgroundTexture;
        private DrawableTexture _inputAreaTexture;
        private DrawableText _text;
        private ComplexDrawable _finalTexture;

        private string _emptyText = "Type Here...";
    }
}
