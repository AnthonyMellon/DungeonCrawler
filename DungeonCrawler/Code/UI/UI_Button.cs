using DungeonCrawler.Code.Input;
using DungeonCrawler.Code.Utils;
using DungeonCrawler.Code.Utils.Drawables;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using DungeonCrawler.Code.UI.Utils;

namespace DungeonCrawler.Code.UI
{
    internal class UI_Button : UIComponent
    {
        #region publics
        public Action OnClick;


        public UI_Button(
            AnchorPoints anchorPoints,
            Padding padding,
            Offset offset,
            Color baseColor,
            Color hoverColor,
            Texture2D texture,
            string text,
            DrawManager.DrawTargets drawTarget,
            FitTypes fitType) :
            base(anchorPoints, padding, offset, drawTarget)
        {
            _baseColor = baseColor;
            _hoverColor = hoverColor;
            _texture = texture;
            _text = text;            

            OnDrawRectangleUpdated += UpdateDrawRectangle;
            BuildButton();
        }

        public UI_Button(
            AnchorPoints anchorPoints,
            Padding padding,
            Offset offset,
            Color baseColor,
            Color hoverColor,
            string text,
            DrawManager.DrawTargets drawTarget,
            FitTypes fitType) :
            base(anchorPoints, padding, offset, drawTarget)
        {
            _baseColor = baseColor;
            _hoverColor = hoverColor;
            _texture = DefaultContent.DefaultCapsule;
            _text = text;

            OnDrawRectangleUpdated += UpdateDrawRectangle;
            BuildButton();
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
        private ComplexDrawable _buttonTexture;

        private void BuildButton()
        {
            DrawableTexture buttonBackground = new DrawableTexture(
                _texture,
                Point.Zero,
                Color.White,
                GameConstants.GameLayers.UI_Texutre);
            _backgroundTexture = buttonBackground;

            DrawableText buttonText = new DrawableText(
                _text,
                Point.Zero,
                Color.Black);
            buttonText.CenterTextToRectangle(buttonBackground.Rectangle);

            _buttonTexture = new ComplexDrawable(GameValues.GraphicsDevice, DrawTarget);
            _buttonTexture.AddDrawables(new List<Drawable>
            {
                buttonBackground,
                buttonText
            });
        }

        private void UpdateDrawRectangle()
        {
            if (_backgroundTexture == null) return;

            _buttonTexture.Position = DrawRectangle.Location;
            _buttonTexture.Size = DrawRectangle.Size;
        }

        private void CheckForHover()
        {
            Rectangle mouseRectangle = new Rectangle(InputProvider.MousePosition, new Point(1, 1));
            _isHovering = InputProvider.IsMouseInWindow && DrawRectangle.Intersects(mouseRectangle);
        }

        private void Highlight()
        {
            Color highlightColor = _isHovering ? _hoverColor : _baseColor;
            if(_backgroundTexture.Color != highlightColor) _backgroundTexture.Color = highlightColor;
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
