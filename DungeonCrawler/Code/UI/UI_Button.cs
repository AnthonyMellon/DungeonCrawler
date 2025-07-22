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
        public Action OnClick;

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

            _butttonText = new DrawableText(text, Vector2.Zero, Color.Black, textFont, GameConstants.GameLayers.UI_Text);
            OnDrawRectangleUpdated += UpdateDestinationRectangle;

            _backgroundTexture = new DrawableTexture(texture, DrawRectangle, baseColor, GameConstants.GameLayers.UI_Texutre);
        }

        private bool _isHovering;
        private bool _prevMouse;
        private bool _currMouse;

        private Color _baseColor;
        private Color _hoverColor;        

        private DrawableText _butttonText;
        private DrawableTexture _backgroundTexture;

        protected override void Draw(GameTime gametime, Camera camera)
        {           
            if (_backgroundTexture.Texture == null) _backgroundTexture.Texture = DefaultContent.DefaultCapsule;

            camera.DrawTexture(_backgroundTexture);
            camera.DrawText(_butttonText);

        }

        protected override void Update(GameTime gametime)
        {
            _prevMouse = _currMouse;
            _currMouse = InputProvider.MouseLeftPressed;

            CheckForHover();
            Highlight();
            CheckForClick();
        }

        private void UpdateDestinationRectangle()
        {
            if (_backgroundTexture == null) return;

            _backgroundTexture.DestinationRectangle = DrawRectangle;
            _butttonText.CenterTextToRectangle(DrawRectangle);
        }

        private void CheckForHover()
        {
            Rectangle mouseRectangle = new Rectangle(InputProvider.MousePosition, new Point(1, 1));
            _isHovering = InputProvider.IsMouseInWindow && DrawRectangle.Intersects(mouseRectangle);
        }

        private void Highlight()
        {
            _backgroundTexture.Color = _isHovering ? _hoverColor : _baseColor;
        }

        private void CheckForClick()
        {
            if (!_isHovering) return;

            if (_currMouse && !_prevMouse)
            {
                OnClick?.Invoke();
            }
        }
    }
}
