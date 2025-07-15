using DungeonCrawler.Code.Input;
using DungeonCrawler.Code.Utils;
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
        /// <summary>
        /// The size of the text string (not the font size)
        /// </summary>
        private Vector2 _textSize;

        private Color _baseColor;
        private Color _hoverColor;
        private Texture2D _texture;

        /// <summary>
        /// Text to be drawn on button
        /// </summary>
        public string Text
        {
            get
            {
                return b_text;
            }
            set
            {
                b_text = value;
                UpdateTextSize();
            }
        }
        private string b_text;

        /// <summary>
        /// Font for text being drawn on button
        /// </summary>
        public SpriteFont TextFont
        {
            get
            {
                if (b_textFont == null) return DefaultContent.DefaultFont;

                return b_textFont;
            }
            set
            {
                b_textFont = value;
                UpdateTextSize();
            }
        }
        private SpriteFont b_textFont;


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
            Text = text;
            TextFont = textFont;
        }

        protected override void Draw(GameTime gametime, SpriteBatch graphics)
        {
            Texture2D drawTexture = _texture == null ? DefaultContent.DefaultCapsule : _texture;

            // Background
            graphics.Draw(drawTexture, ScreenRectangle, _currentColor);

            // Text
            Vector2 textPos = new Vector2( // Center Text
                (ScreenRectangle.X + ScreenRectangle.Width / 2) - _textSize.X / 2,
                (ScreenRectangle.Y + ScreenRectangle.Height / 2) - _textSize.Y / 2);
            graphics.DrawString(TextFont, Text, textPos, Color.Black);

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

        /// <summary>
        /// Update the text size (not the font size)
        /// </summary>
        private void UpdateTextSize()
        {
            if (Text == null || TextFont == null)
            {
                _textSize = new Vector2(0, 0);
            }
            else
            {
                _textSize = TextFont.MeasureString(Text);
            }
        }
    }
}
