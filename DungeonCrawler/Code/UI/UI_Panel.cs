using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DungeonCrawler.Code.Utils;
using System.Collections.Generic;

namespace DungeonCrawler.Code.UI
{
    internal class UI_Panel : UIComponent
    {
        private Texture2D _texture;
        private Color? _color;

        public UI_Panel(
            Vector4 anchorPoints,
            Point4 padding,
            Point offset,
            Texture2D texture = null,
            Color? color = null) :
            base(anchorPoints, padding, offset)
        {
            _texture = texture;
            _color = color;
        }

        protected override void Draw(GameTime gametime, Camera camera)
        {
            // only draw if there is a texture to draw, this allows for invisible panels
            if (_texture != null)
            {
                // Default to white color if none is given
                Color color = _color != null ? _color.Value : Color.White;

                graphics.Draw(_texture, ScreenRectangle, color);                
            }
        }
    }
}
