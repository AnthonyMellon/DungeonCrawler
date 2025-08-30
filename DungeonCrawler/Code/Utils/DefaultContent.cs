using DungeonCrawler.Code.Data;
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

        // Data      
        public static TileData[] GetTileData => GameValues.GameContent.Load<TileData[]>("Data/TileData");
        public static RoomData[] GetRoomData => GameValues.GameContent.Load<RoomData[]>("Data/RoomData");

        // Sprite Sheets
        public static SpriteSheetData BasicCharacterSpriteSheetData { get; private set; }
        public static SpriteSheetData BasicTilesSpriteSheetData { get; private set; }

        public static void LoadContent()
        {
            // Fonts
            DefaultFont = GameValues.GameContent.Load<SpriteFont>("Fonts/DefaultFont");

            // Textures
            DefaultRectangle = GameValues.GameContent.Load<Texture2D>("Images/Textures/DefaultRectangle");
            DefaultCapsule = GameValues.GameContent.Load<Texture2D>("Images/Textures/DefaultCapsule");

            //Spritesheets
            BasicCharacterSpriteSheetData = GameValues.GameContent.Load<SpriteSheetData>("Data/SpriteSheets/BasicCharacter");
            BasicTilesSpriteSheetData = GameValues.GameContent.Load<SpriteSheetData>("Data/SpriteSheets/BasicTiles");
        }
    }
}
