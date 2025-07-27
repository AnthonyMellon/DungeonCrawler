using DungeonCrawler.Code.UI.Utils;
using DungeonCrawler.Code.Utils;
using DungeonCrawler.Code.Utils.Drawables;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonCrawler.Code.UI
{
    internal class UI_Panel : UIComponent
    {
        #region publics        
        public UI_Panel(
            AnchorPoints anchorPoints,
            Padding padding,
            Point offset,
            Texture2D texture,
            Color color,
            GameConstants.GameLayers layer = GameConstants.GameLayers.Bottom,
            DrawManager.DrawTargets drawTarget = DrawManager.DrawTargets.None,
            FitTypes fitType = FitTypes.Parent) :
            base(anchorPoints, padding, offset, drawTarget, fitType)
        {
            if (texture == null) texture = DefaultContent.DefaultRectangle;

            _backgroundTexture = new DrawableTexture(
                texture,
                DrawRectangle.Location,
                color,
                layer,
                drawTarget
                );
            OnDrawRectangleUpdated += UpdateDrawRectangle;
        }

        public UI_Panel(
            AnchorPoints anchorPoints,
            Padding padding,
            Point offset,
            DrawManager.DrawTargets drawTarget = DrawManager.DrawTargets.None,
            FitTypes fitType = FitTypes.Parent) :
            base(anchorPoints, padding, offset, drawTarget, fitType)
        {
            _backgroundTexture = null;
            OnDrawRectangleUpdated += UpdateDrawRectangle;
        }
        #endregion

        #region privates
        private DrawableTexture _backgroundTexture;

        private void UpdateDrawRectangle()
        {
            if (_backgroundTexture == null) return;

            _backgroundTexture.Position = DrawRectangle.Location;
            _backgroundTexture.Size = DrawRectangle.Size;
        }
        #endregion
    }
}
