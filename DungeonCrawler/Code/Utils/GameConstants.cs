using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace DungeonCrawler.Code.Utils
{
    internal static class GameConstants
    {
        #region GameLayers

        // Oder matters here. Top gets drawn last
        public enum GameLayers
        {
            Top,
            UI_Foregound,
            UI_Background,
            World_Player,
            World_Enemies,
            World_Background,
            Bottom
        }
        private static int _NumberOfGameLayers = Enum.GetValues<GameLayers>().Length;
        public static float GameLayerToLayer(GameLayers layer) => (float)layer / _NumberOfGameLayers;
        #endregion

        #region Generic Entities        

        public static string PointToDirection(Point point)
        {
            if (point.X > 0) return EntityDirections.RIGHT;
            else if (point.X < 0) return EntityDirections.LEFT;
            else if (point.Y < 0) return EntityDirections.BACK;
            else return EntityDirections.FOWARD;
        }
        #endregion

        #region Sprite Sizing
        public static readonly Point ENTITY_SPRITE_SIZE = new Point(256, 256);
        public static readonly Point TILE_SPRITE_SIZE = new Point(256, 256);
        #endregion

        #region SpritesheetNames
        public static class EntityDirections
        {
            public static readonly string FOWARD = "Front";
            public static readonly string BACK = "Back";
            public static readonly string LEFT = "Left";
            public static readonly string RIGHT = "Right";
        }

        public static class Tiles
        {
            public static readonly string FLOOR = "Floor";
            public static readonly string WALL = "Wall";
            public static readonly string HEAL = "Heal";
            public static readonly string DAMAGE = "Damage";

            public static string TileIDToTileName(int id)
            {
                if (!_tileIdToTileName.ContainsKey(id)) return FLOOR; // Default to floor tile

                return _tileIdToTileName[id];

            }
            private static Dictionary<int, string> _tileIdToTileName = new Dictionary<int, string>()
            {
                {0, FLOOR},
                {1, WALL},
                {2, HEAL},
                {3, DAMAGE}
            };
        }
        #endregion

        #region Scene Strings
        public static class SceneNames
        {
            public static readonly string MainMenu = "MainMenu";
            public static readonly string Game = "Game";
            public static readonly string GameUI = "GameUI";

#if DEVELOPMENT
            public static readonly string DevMenu = "DevMenu";
#endif
        }
        #endregion

        #region Object Strings
        public static readonly string MAIN_CAMERA = "MainCamera";
        #endregion        

        #region Path Strings
        public const string PLAYER_SPRITESHEET_PATH = "Images/Spritesheets/DebugCharacter";
        public const string ENEMY_SPRITESHEET_PATH = "Images/Spritesheets/DebugCharacter";
        public const string MENU_BACKGROUND_PATH = "Images/TempMenuBackground";
        public const string TILES_SPRITESHEET_PATH = "Images/Spritesheets/BasicTiles";
        #endregion

        #region Colors
        public static readonly Color DEFAULT_COLOR = Color.CornflowerBlue;
        #endregion

        public static readonly string WindowTitle = "Dunugeon Crawller Game";
    }
}
