using DungeonCrawler.Code.UI;
using DungeonCrawler.Code.Utils;
using DungeonCrawler.Code.Utils.TileMaps;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
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
            _tileMap.LoadTilesFromText(null);
        }

        protected override void Draw(GameTime gametime, Camera camera)
        {
            if (_tileMap.HasTilesLoaded) _tileMap.DrawAllTiles();
        }

        protected override void Update(GameTime gametime)
        {
            
        }
        #endregion
    }
}
