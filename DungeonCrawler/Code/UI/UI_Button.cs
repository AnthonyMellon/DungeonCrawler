using DungeonCrawler.Code.Input;
using DungeonCrawler.Code.Scenes;
using DungeonCrawler.Code.UI.Utils;
using DungeonCrawler.Code.Utils;
using DungeonCrawler.Code.Utils.Drawables;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace DungeonCrawler.Code.UI
{
    internal class UI_Button : UIComponent
    {
        #region publics
        public Action OnClick;

        public UI_Button(
            Color baseColor,
            Color hoverColor,
            Texture2D texture,
            string text,
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
            _baseColor = baseColor;
            _hoverColor = hoverColor;
            _texture = texture;
            _text = text;

            BuildButton(layer);
        }

        public UI_Button(
            Color baseColor,
            Color hoverColor,
            string text,
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
            _baseColor = baseColor;
            _hoverColor = hoverColor;
            _texture = DefaultContent.DefaultCapsule;
            _text = text;

            BuildButton(layer);
        }
        #endregion

        #region privates
        private bool _isHovering;
        private bool _prevMouse;
        private bool _currMouse;

        private Color _baseColor;
        private Color _hoverColor;

        private Texture2D _texture;
        private string _text;

        private DrawableTexture _backgroundTexture;

        private void BuildButton(GameConstants.GameLayers layer)
        {
            DrawableTexture buttonBackground = new DrawableTexture(
                _texture,
                Point.Zero,
                Color.White,
                GameConstants.GameLayers.Bottom,
                null);
            _backgroundTexture = buttonBackground;

            DrawableTexture foregroundTexture = new DrawableTexture(
                _texture,
                Color.White,
                GameConstants.GameLayers.Bottom,
                AnchorPoints.Fill,
                Size.Negative * 10,
                Offset.Zero,
                DynamicRectangle.FitTypes.Parent,
                DynamicRectangle.GrowFroms.Auto,
                buttonBackground.Rectangle,
                null);


            DrawableText buttonText = new DrawableText(
                _text,
                Point.Zero,
                Color.Black,
                GameConstants.GameLayers.Bottom,
                null);
            buttonText.CenterTextToRectangle(Rectangle.Rectangle);

            ComplexDrawable drawTexture = new ComplexDrawable(GameValues.GraphicsDevice, layer, Scene);
            drawTexture.AddDrawables(new List<Drawable>
            {
                buttonBackground,
                foregroundTexture,
                buttonText
            });
            DrawTexture = drawTexture;
        }

        private void CheckForHover()
        {
            Rectangle mouseRectangle = new Rectangle(InputProvider.MousePosition, new Point(1, 1));
            _isHovering = InputProvider.IsMouseInWindow && Rectangle.Rectangle.Intersects(mouseRectangle);
        }

        private void Highlight()
        {
            Color highlightColor = _isHovering ? _hoverColor : _baseColor;
            if (_backgroundTexture.Color != highlightColor) _backgroundTexture.Color = highlightColor;
        }

        private void CheckForClick()
        {
            if (!_isHovering) return;

            if (_currMouse && !_prevMouse)
            {
                OnClick?.Invoke();
            }
        }

        protected override void Update(GameTime gametime)
        {
            _prevMouse = _currMouse;
            _currMouse = InputProvider.MouseLeftPressed;

            CheckForHover();
            Highlight();
            CheckForClick();
        }
        #endregion
    }
}
