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
            _tileMap.LoadTilesFromRoomData(DefaultContent.GetRoomData[0]); //TODO: this is a temp job loading the first roomData, eventually I'll want to pass a name or id or something... 
        }

        protected override void Update(GameTime gametime)
        {

        }
        #endregion
    }
}
