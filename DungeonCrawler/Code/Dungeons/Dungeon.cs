using DungeonCrawler.Code.Scenes;
using DungeonCrawler.Code.Utils;
using DungeonCrawler.Code.Utils.TileMaps;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace DungeonCrawler.Code.Dungeons
{
    internal class Dungeon : Dynamic
    {
        #region publics
        public Dungeon(Scene scene, bool enabled = true) :
            base(enabled)
        {
            _scene = scene;

            _tileMap = new TileMap(
                new SpriteSheet(DefaultContent.BasicTilesSpriteSheetData),
                _scene);
        }

        public void BuildDungeon()
        {
            LoadTiles();
        }
        #endregion

        #region privates
        private TileMap _tileMap;
        private Scene _scene;

        private void LoadTiles()
        {
            _tileMap.LoadTilesFromText(
                new List<List<string>>()
                {
                    new List<string>() {GameConstants.Tiles.WALL, GameConstants.Tiles.WALL, GameConstants.Tiles.WALL, GameConstants.Tiles.WALL, GameConstants.Tiles.WALL },
                    new List<string>() {GameConstants.Tiles.WALL, GameConstants.Tiles.FLOOR, GameConstants.Tiles.FLOOR, GameConstants.Tiles.FLOOR, GameConstants.Tiles.WALL },
                    new List<string>() {GameConstants.Tiles.WALL, GameConstants.Tiles.HEAL, GameConstants.Tiles.FLOOR, GameConstants.Tiles.DAMAGE, GameConstants.Tiles.WALL },
                    new List<string>() {GameConstants.Tiles.WALL, GameConstants.Tiles.FLOOR, GameConstants.Tiles.FLOOR, GameConstants.Tiles.FLOOR, GameConstants.Tiles.WALL },
                    new List<string>() {GameConstants.Tiles.WALL, GameConstants.Tiles.WALL, GameConstants.Tiles.WALL, GameConstants.Tiles.WALL, GameConstants.Tiles.WALL }
                }
                );
        }

        protected override void Update(GameTime gametime)
        {

        }
        #endregion
    }
}
