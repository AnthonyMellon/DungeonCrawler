using DungeonCrawler.Code.Input;
using DungeonCrawler.Code.Utils;
using DungeonCrawler.Code.Utils.Drawables;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace DungeonCrawler.Code.UI
{
    internal class UI_Button : UIComponent
    {
        private bool _isHovering;
        private Color _currentColor;
        /// <summary>
        /// Acion to trigger when the mouse is clicked on
        /// </summary>
        public Action OnClick;
        /// <summary>
        /// Last frames mouse status
        /// </summary>
        private bool _prevMouse;
        /// <summary>
        /// Current frames mouse status
        /// </summary>
        private bool _currMouse;

        private Color _baseColor;
        private Color _hoverColor;
        private Texture2D _texture;

        private DrawableText _text;

        public UI_Button(
            Vector4 anchorPoints,
            Point4 padding,
            Point offset,
            Color baseColor,
            Color hoverColor,
            Texture2D texture,
            string text,
            SpriteFont textFont) :
            base(anchorPoints, padding, offset)
        {
            _baseColor = baseColor;
            _hoverColor = hoverColor;
            _texture = texture;

            _text = new DrawableText(text, Vector2.Zero, Color.Black, textFont);
            OnScreenRectangleUpdated += () => _text.CenterTextToRectangle(ScreenRectangle);
        }

        protected override void Draw(GameTime gametime, SpriteBatch graphics)
        {
            Texture2D drawTexture = _texture == null ? DefaultContent.DefaultCapsule : _texture;

            // Background
            graphics.Draw(drawTexture, ScreenRectangle, _currentColor);

            // Text
            graphics.DrawString(_text.Font, _text.Text, _text.Position, Color.Black);
        }

        protected override void Update(GameTime gametime)
        {
            _prevMouse = _currMouse;
            _currMouse = InputProvider.MouseLeftPressed;

            CheckForHover();
            Highlight();
            CheckForClick();
        }

        /// <summary>
        /// Check if the mouse is hovering over the button
        /// </summary>
        private void CheckForHover()
        {
            Rectangle mouseRectangle = new Rectangle(InputProvider.MousePosition, new Point(1, 1));
            _isHovering = InputProvider.IsMouseInWindow && ScreenRectangle.Intersects(mouseRectangle);
        }

        /// <summary>
        /// Highlight the button if the mouse is hovering over it
        /// </summary>
        private void Highlight()
        {
            _currentColor = _isHovering ? _hoverColor : _baseColor;
        }

        /// <summary>
        /// Check if the button was clicked on
        /// </summary>
        private void CheckForClick()
        {
            // THe button can't be interacted with if it's not being hovered over
            if (!_isHovering) return;

            if (_currMouse && !_prevMouse)
            {
                OnClick?.Invoke();
            }
        }
    }
}
