using DungeonCrawler.Code.Utils.TileMaps;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using System.Text.Json;

namespace DungeonCrawler.Code.Utils
{
    internal static class DefaultContent
    {
        // Fonts
        public static SpriteFont DefaultFont { get; private set; }

        // Textures
        public static Texture2D DefaultRectangle { get; private set; }
        public static Texture2D DefaultCapsule { get; private set; }

        // Tile Data
        public static TileData[] TileData { get; private set; }

        public static void LoadContent(ContentManager content)
        {
            // Fonts
            DefaultFont = content.Load<SpriteFont>("Fonts/DefaultFont");

            // Textures
            DefaultRectangle = content.Load<Texture2D>("Images/Textures/DefaultRectangle");
            DefaultCapsule = content.Load<Texture2D>("Images/Textures/DefaultCapsule");

            // TileData
            //TileData = content.Load<TileData[]>("Data/TileData");
        }
    }
}
