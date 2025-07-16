using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler.Code.Utils.Drawables
{
    internal class DrawableText
    {
        public string Text
        { 
            get
            {
                return _text;
            }
            set
            {
                _text = value;
                UpdateTextSize();
            }
        }

        public SpriteFont Font
        {
            get 
            {
                return _font;
            }
            set
            {
                _font = value;
                UpdateTextSize();
            }
        }
        public Vector2 Size { get; private set; }
        public Vector2 Position;
        public Color Color;

        public DrawableText(string text, Vector2 position, Color color, SpriteFont font)
        {
            Text = text;
            Position = position;
            Color = color;
            Font = font;
        }

        public DrawableText(string text, Vector2 position, Color color)
        {
            Text = text;
            Position = position;
            Color = color;
            Font = DefaultContent.DefaultFont;
        }

        public void CenterTextToRectangle(Rectangle targetRectangle)
        {
            Position = new Vector2(
                (targetRectangle.X + targetRectangle.Width / 2) - Size.X / 2,
                (targetRectangle.Y + targetRectangle.Height / 2) - Size.Y / 2);
        }

        private string _text;
        private SpriteFont _font;

        private void UpdateTextSize()
        {
            if(Text == null || Font == null)
            {
                Size = Vector2.Zero;
            }
            else
            {
                Size = Font.MeasureString(Text);
            }
        }
    }
}
