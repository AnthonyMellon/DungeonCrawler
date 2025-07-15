using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonCrawler.Code.Utils.Drawables
{
    internal interface IWorldDrawable
    {
        public Texture2D Texture { get; protected set; }
        public Vector2 Position { get; protected set; }
        public Rectangle SourceRectangle { get; protected set; }
        public Color Color { get; protected set; }
        public float Rotation { get; protected set; }
        public Vector2 Origin { get; protected set; }
        public float Scale { get; protected set; }
        public GameConstants.GameLayers Layer { get; protected set; }
    }
}
