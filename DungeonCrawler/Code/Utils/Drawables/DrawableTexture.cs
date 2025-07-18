using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonCrawler.Code.Utils.Drawables
{
    internal class DrawableTexture
    {
        public Texture2D Texture { get; set; }
        public Rectangle DestinationRectangle { get; set; }
        public Color Color { get; set; }
        public GameConstants.GameLayers Layer { get; set; }

        public DrawableTexture(Texture2D texture, Rectangle destinationRectangle, Color color, GameConstants.GameLayers layer)
        {
            Texture = texture;
            DestinationRectangle = destinationRectangle;
            Color = color;
            Layer = layer;
        }
        public DrawableTexture(Texture2D texture, GameConstants.GameLayers layer)
        {
            Texture = texture;
            Layer = layer;
        }
    }
}
