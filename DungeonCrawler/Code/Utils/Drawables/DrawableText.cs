using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
                if (_font == null) return DefaultContent.DefaultFont;
                return _font;
            }
            set
            {
                _font = value;
                UpdateTextSize();
            }
        }
        public Vector2 Size { get; private set; }
        public Vector2 Position { get; private set; }
        public Color Color { get; private set; }
        public GameConstants.GameLayers Layer { get; private set; }

        public DrawableText(string text, Vector2 position, Color color, SpriteFont font, GameConstants.GameLayers layer)
        {
            Text = text;
            Position = position;
            Color = color;
            Font = font;
            Layer = layer;
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

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(
                Font,
                Text,
                Position,
                Color,
                0,              // Rotation
                Vector2.Zero,   // Origin
                1,              // Scale        currently no use for these values
                SpriteEffects.None,
                GameConstants.GameLayerToLayer(Layer)
            );
        }

        private string _text;
        private SpriteFont _font;

        private void UpdateTextSize()
        {
            if (Text == null || Font == null)
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
