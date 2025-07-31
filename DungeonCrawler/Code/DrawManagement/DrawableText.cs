using DungeonCrawler.Code.Scenes;
using DungeonCrawler.Code.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace DungeonCrawler.Code.DrawManagement
{
    internal class DrawableText : Drawable
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
                OnChange?.Invoke();
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
                OnChange?.Invoke();
            }
        }

        public DrawableText(
            string text,
            Point position,
            Color color,
            SpriteFont font,
            GameConstants.GameLayers layer,
            Scene scene,
            bool visible = true) :
            base(layer, scene, visible)
        {
            Text = text;
            Position = position;
            Color = color;
            Font = font;
            Layer = layer;
        }

        public DrawableText(
            string text,
            Point position,
            Color color,
            GameConstants.GameLayers layer,
            Scene scene,
            bool visible = true) :
            base(layer, scene, visible)
        {
            Text = text;
            Position = position;
            Color = color;
            Font = DefaultContent.DefaultFont;
        }

        public void CenterTextToRectangle(Rectangle targetRectangle)
        {
            Vector2 newPosition = new Vector2(
                targetRectangle.Center.X - Size.X / 2,
                targetRectangle.Center.Y - Size.Y / 2);
            Position = new Point((int)newPosition.X, (int)newPosition.Y);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            string text = Text == null ? string.Empty : Text;
            spriteBatch.DrawString(
                Font,
                text,
                new Vector2(Position.X, Position.Y),
                Color,
                0,              // Rotation
                Vector2.Zero,   // Origin
                1,              // Scale        currently no use for these values
                SpriteEffects.None,
                GameConstants.GameLayerToLayer(Layer)
            );
        }

        public override void Draw(SpriteBatch spritebatch, Rectangle destinationRectangle)
        {
            string text = Text == null ? string.Empty : Text;
            spritebatch.DrawString(
                Font,
                text,
                new Vector2(Position.X, Position.Y),
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
                Size = Point.Zero;
            }
            else
            {
                Vector2 sizeVector = Font.MeasureString(Text);
                Size = new Point((int)Math.Ceiling(sizeVector.X), (int)Math.Ceiling(sizeVector.Y));
            }
        }
    }
}
