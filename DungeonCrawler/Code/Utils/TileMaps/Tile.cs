using DungeonCrawler.Code.DrawManagement;
using DungeonCrawler.Code.Scenes;
using Microsoft.Xna.Framework;

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
            Scene scene,
            GameConstants.GameLayers layer = GameConstants.GameLayers.World_Background)
        {
            Sprite = new DrawableSprite(
                sheet,
                position,
                tileName,
                layer,
                scene);
        }
        #endregion

        #region privates        
        #endregion
    }
}
