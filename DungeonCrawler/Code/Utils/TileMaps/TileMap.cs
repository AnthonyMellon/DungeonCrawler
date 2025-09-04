using DungeonCrawler.Code.Data;
using DungeonCrawler.Code.Scenes;
using DungeonCrawler.Code.UI;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace DungeonCrawler.Code.Utils.TileMaps
{
    internal class TileMap
    {
        #region publics
        public bool HasTilesLoaded = false;

        public void LoadTilesFromRoomData(RoomData roomData)
        {
            _tiles?.Clear();
            HasTilesLoaded = false;

            if (roomData == null) return;
            if (roomData.Tiles == null) return;

            List<List<int>> tileData = roomData.Tiles;

            for (int y = 0; y < tileData.Count; y++)
            {                
                List<int> rowData = tileData[y];
                List<Tile> rowTiles = new List<Tile>();

                for (int x = 0; x < rowData.Count; x++)
                {
                    Point tilePos = new Point(x * _tileSize, y * _tileSize);

                    rowTiles.Add(new Tile(
                        _spriteSheet,
                        tilePos,
                        GameConstants.Tiles.TileIDToTileName(rowData[x]),
                        _scene,
                        _layer
                        ));
                }
                _tiles.Add(rowTiles);
            }

        }

        public TileMap(SpriteSheet spriteSheet, Scene scene)
        {
            _spriteSheet = spriteSheet;
            _tiles = new List<List<Tile>>();
            _scene = scene;
        }
        #endregion

        #region privates
        private List<List<Tile>> _tiles;
        private SpriteSheet _spriteSheet;
        private GameConstants.GameLayers _layer = GameConstants.GameLayers.World_Background;
        private Camera _camera;
        private int _tileSize = 256;
        private Scene _scene;

        private Camera Camera
        {
            get
            {
                if (_camera == null) _camera = ObjectBin.GetObject(GameConstants.MAIN_CAMERA) as Camera;
                return _camera;
            }
        }
        #endregion
    }
}
