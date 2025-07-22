using DungeonCrawler.Code.Utils.Drawables;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler.Code.Utils.TileMaps
{
    internal class Tile
    {
        #region publics
        public DrawableSprite Sprite { get; private set; }

        public void UpdateTile(string newTileName)
        {
            Sprite.CurrentSpriteName = newTileName;
        }

        public Tile(
            SpriteSheet sheet,
            Point position,
            string tileName,
            GameConstants.GameLayers layer = GameConstants.GameLayers.World_Background)
        {
            Sprite = new DrawableSprite(
                sheet,
                position,
                tileName,
                layer);
        }
        #endregion

        #region privates        
        #endregion
    }
}
