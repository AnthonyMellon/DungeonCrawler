using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace DungeonCrawler.Code.Utils.Drawables
{
    internal class DrawableSprite
    {
        public Texture2D Texture { get; private set; }
        public Rectangle DestinationRectangle { get; private set; }
        public Rectangle SourceRectangle { get; private set; }
        public Color Color { get; private set; }
        public GameConstants.GameLayers Layer { get; private set; }

        public DrawableSprite(Texture2D texture, Rectangle destinationRectangle, Rectangle sourceRectangle, Color color, GameConstants.GameLayers layer)
        {
            Texture = texture;
            DestinationRectangle = destinationRectangle;
            SourceRectangle = sourceRectangle;
            Color = color;
            Layer = layer;
        }

        public DrawableSprite(Texture2D texture, Rectangle destinationRectangle, Color color, GameConstants.GameLayers layer)
        {
            Texture = texture;
            DestinationRectangle = destinationRectangle;
            SourceRectangle = texture.Bounds;
            Color = color;
            Layer = layer;
        }

        public void SetColor(Color color)
        {
            Color = color;
        }
    }
}
