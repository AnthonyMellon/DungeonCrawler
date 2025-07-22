using DungeonCrawler.Code.UI;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace DungeonCrawler.Code.Utils.TileMaps
{
    internal class TileMap
    {
        #region publics
        public bool HasTilesLoaded = false;

        public void LoadTilesFromText(List<List<string>> tileMapText)
        {
            _tiles?.Clear();
            HasTilesLoaded = false;

            if (tileMapText == null) return;

            for (int x = 0; x < tileMapText.Count; x++)
            {
                List<string> rowAsText = tileMapText[x];
                List<Tile> rowAsTiles = new List<Tile>();
                for (int y = 0; y < rowAsText.Count; y++)
                {
                    Point tilePosition = new Point(x * _tileSize, y * _tileSize);

                    rowAsTiles.Add(new Tile(
                        _spriteSheet,
                        tilePosition,
                        rowAsText[y],
                        _layer));
                }
            }

            HasTilesLoaded = true;
        }

        public void DrawAllTiles()
        {
            for (int x = 0; x < _tiles.Count; x++)
            {
                for (int y = 0; y < _tiles[x].Count; y++)
                {
                    _camera.DrawSprite(_tiles[x][y].Sprite);
                }
            }
        }

        public TileMap(SpriteSheet spriteSheet)
        {
            _spriteSheet = spriteSheet;
        }
        #endregion

        #region privates
        private List<List<Tile>> _tiles;
        private SpriteSheet _spriteSheet;
        private GameConstants.GameLayers _layer = GameConstants.GameLayers.World;
        private Camera _camera;
        private int _tileSize = 256;

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
