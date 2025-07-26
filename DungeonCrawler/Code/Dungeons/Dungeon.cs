using DungeonCrawler.Code.UI;
using DungeonCrawler.Code.Utils;
using DungeonCrawler.Code.Utils.TileMaps;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler.Code.Dungeons
{
    internal class Dungeon : Dynamic
    {
        #region publics
        public Dungeon(bool enabled = true) :
            base(enabled)
        {
            _tileMap = new TileMap(
                new SpriteSheet(
                    GameConstants.TILES_SPRITESHEET_PATH,
                    GameConstants.TILE_SPRITE_RECTANGLES));
        }

        public void BuildDungeon()
        {
            LoadTiles();            
        }
        #endregion

        #region privates
        private TileMap _tileMap;

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
