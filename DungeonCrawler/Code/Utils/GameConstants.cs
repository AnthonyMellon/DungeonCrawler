using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace DungeonCrawler.Code.Utils
{
    internal static class GameConstants
    {
        #region GameLayers
        public enum GameLayers
        {
            UI,
            World,
        }
        private static int _NumberOfGameLayers = Enum.GetValues<GameLayers>().Length;
        public static float GameLayerToLayer(GameLayers layer) => (float)layer / _NumberOfGameLayers;
        #endregion

        #region Generic Entities
        public static readonly Point ENTITY_SPRITE_SIZE = new Point(256, 256);
        public static class EntityDirections
        {
            public static readonly string foward = "Front";
            public static readonly string Back = "Back";
            public static readonly string Left = "Left";
            public static readonly string Right = "Right";
        }
        #endregion

        #region Sprite Sheet Rectangles
        
        public static readonly Dictionary<string, Rectangle> PLAYER_SPRITE_RECTANGLES = new Dictionary<string, Rectangle>()
        {
            {EntityDirections.foward, new Rectangle(new Point(ENTITY_SPRITE_SIZE.X * 0, ENTITY_SPRITE_SIZE.Y * 0), ENTITY_SPRITE_SIZE) },
            {EntityDirections.Back, new Rectangle(new Point(ENTITY_SPRITE_SIZE.X * 1, ENTITY_SPRITE_SIZE.Y * 0), ENTITY_SPRITE_SIZE) },
            {EntityDirections.Left, new Rectangle(new Point(ENTITY_SPRITE_SIZE.X * 0, ENTITY_SPRITE_SIZE.Y * 1), ENTITY_SPRITE_SIZE) },
            {EntityDirections.Right, new Rectangle(new Point(ENTITY_SPRITE_SIZE.X * 1, ENTITY_SPRITE_SIZE.Y * 1), ENTITY_SPRITE_SIZE) },
        };
        public static readonly Dictionary<string, Rectangle> ENEMY_SPRITE_RECTANGLES = new Dictionary<string, Rectangle>()
        {
            {EntityDirections.foward, new Rectangle(new Point(ENTITY_SPRITE_SIZE.X * 0, ENTITY_SPRITE_SIZE.Y * 0), ENTITY_SPRITE_SIZE) },
            {EntityDirections.Back, new Rectangle(new Point(ENTITY_SPRITE_SIZE.X * 1, ENTITY_SPRITE_SIZE.Y * 0), ENTITY_SPRITE_SIZE) },
            {EntityDirections.Left, new Rectangle(new Point(ENTITY_SPRITE_SIZE.X * 0, ENTITY_SPRITE_SIZE.Y * 1), ENTITY_SPRITE_SIZE) },
            {EntityDirections.Right, new Rectangle(new Point(ENTITY_SPRITE_SIZE.X * 1, ENTITY_SPRITE_SIZE.Y * 1), ENTITY_SPRITE_SIZE) },
        };
        #endregion

        #region Scene Strings
        public static readonly string MainMenu = "MainMenu";
        public static readonly string Game = "Game";
        #endregion

        #region Object Strings
        public static readonly string MAIN_CAMERA = "MainCamera";
        #endregion        

        #region Path Strings
        public const string PLAYER_SPRITESHEET_PATH = "Images/Spritesheets/TempCharacter";
        public const string ENEMY_SPRITESHEET_PATH = "Images/Spritesheets/TempEnemy";
        public const string MENU_BACKGROUND_PATH = "Images/TempMenuBackground";
        #endregion
    }
}
