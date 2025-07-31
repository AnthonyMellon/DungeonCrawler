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
        public static class EntityDirections
        {
            public static readonly string FOWARD = "Front";
            public static readonly string BACK = "Back";
            public static readonly string LEFT = "Left";
            public static readonly string RIGHT = "Right";
        }

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

        #region Sprite Sheet Rectangles

        public static readonly Dictionary<string, Rectangle> PLAYER_SPRITE_RECTANGLES = new Dictionary<string, Rectangle>()
        {
            {EntityDirections.FOWARD, new Rectangle(new Point(ENTITY_SPRITE_SIZE.X * 2, ENTITY_SPRITE_SIZE.Y * 0), ENTITY_SPRITE_SIZE) },
            {EntityDirections.BACK, new Rectangle(new Point(ENTITY_SPRITE_SIZE.X * 0, ENTITY_SPRITE_SIZE.Y * 0), ENTITY_SPRITE_SIZE) },
            {EntityDirections.LEFT, new Rectangle(new Point(ENTITY_SPRITE_SIZE.X * 1, ENTITY_SPRITE_SIZE.Y * 0), ENTITY_SPRITE_SIZE) },
            {EntityDirections.RIGHT, new Rectangle(new Point(ENTITY_SPRITE_SIZE.X * 3, ENTITY_SPRITE_SIZE.Y * 0), ENTITY_SPRITE_SIZE) },
        };
        public static readonly Dictionary<string, Rectangle> ENEMY_SPRITE_RECTANGLES = new Dictionary<string, Rectangle>()
        {
            {EntityDirections.FOWARD, new Rectangle(new Point(ENTITY_SPRITE_SIZE.X * 2, ENTITY_SPRITE_SIZE.Y * 0), ENTITY_SPRITE_SIZE) },
            {EntityDirections.BACK, new Rectangle(new Point(ENTITY_SPRITE_SIZE.X * 0, ENTITY_SPRITE_SIZE.Y * 0), ENTITY_SPRITE_SIZE) },
            {EntityDirections.LEFT, new Rectangle(new Point(ENTITY_SPRITE_SIZE.X * 1, ENTITY_SPRITE_SIZE.Y * 0), ENTITY_SPRITE_SIZE) },
            {EntityDirections.RIGHT, new Rectangle(new Point(ENTITY_SPRITE_SIZE.X * 3, ENTITY_SPRITE_SIZE.Y * 0), ENTITY_SPRITE_SIZE) },
        };
        public static readonly Dictionary<string, Rectangle> TILE_SPRITE_RECTANGLES = new Dictionary<string, Rectangle>()
        {
            {Tiles.FLOOR, new Rectangle(new Point(TILE_SPRITE_SIZE.X * 0, TILE_SPRITE_SIZE.Y * 0), TILE_SPRITE_SIZE) },
            {Tiles.WALL, new Rectangle(new Point(TILE_SPRITE_SIZE.X * 1, TILE_SPRITE_SIZE.Y * 0), TILE_SPRITE_SIZE) },
            {Tiles.HEAL, new Rectangle(new Point(TILE_SPRITE_SIZE.X * 0, TILE_SPRITE_SIZE.Y * 1), TILE_SPRITE_SIZE) },
            {Tiles.DAMAGE, new Rectangle(new Point(TILE_SPRITE_SIZE.X * 1, TILE_SPRITE_SIZE.Y * 1), TILE_SPRITE_SIZE) }
        };
        #endregion

        #region Tiles
        public static class Tiles
        {
            public static readonly string FLOOR = "Tile_floor";
            public static readonly string WALL = "Tile_wall";
            public static readonly string HEAL = "Tile_heal";
            public static readonly string DAMAGE = "Tile_damage";
        }
        #endregion

        #region Scene Strings
        public static class SceneNames
        {
            public static readonly string MainMenu = "MainMenu";
            public static readonly string Game = "Game";
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
    }
}
