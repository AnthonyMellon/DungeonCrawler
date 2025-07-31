using DungeonCrawler.Code.DrawManagement;
using DungeonCrawler.Code.Scenes;
using DungeonCrawler.Code.UI.Utils;
using DungeonCrawler.Code.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonCrawler.Code.UI
{
    internal class UI_Panel : UIComponent
    {
        #region publics
        public UI_Panel(
            Texture2D texture,
            Color color,
            GameConstants.GameLayers layer,
            AnchorPoints anchorPoints,
            Size size,
            Offset offset,
            DynamicRectangle.FitTypes fitType,
            DynamicRectangle.GrowFroms growFrom,
            Scene scene,
            bool enabled = true) :
            base(anchorPoints, size, offset, fitType, growFrom, scene, enabled)
        {
            Rectangle.OnRectangleUpdated += UpdateTextureRectangle;

            DrawTexture = new DrawableTexture(
                texture,
                Rectangle.ScreenLocation,
                color,
                layer,
                scene);
            DrawTexture.Size = Rectangle.ScreenSize;
        }

        public UI_Panel(
            AnchorPoints anchorPoints,
            Size size,
            Offset offset,
            DynamicRectangle.FitTypes fitType,
            DynamicRectangle.GrowFroms growFrom,
            Scene scene,
            bool enabled = true) :
            base(anchorPoints, size, offset, fitType, growFrom, scene, enabled)
        {
            Rectangle.OnRectangleUpdated += UpdateTextureRectangle;

            DrawTexture = null;
        }
        #endregion

        #region privates
        private void UpdateTextureRectangle()
        {
            if (DrawTexture == null) return;

            DrawTexture.Position = Rectangle.ScreenLocation;
            DrawTexture.Size = Rectangle.ScreenSize;
        }
        #endregion
    }
}
