﻿using DungeonCrawler.Code.Utils;
using DungeonCrawler.Code.Utils.Drawables;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonCrawler.Code.UI
{
    internal class UI_Panel : UIComponent
    {
        #region publics        
        public UI_Panel(
            Vector4 anchorPoints,
            Point4 padding,
            Point offset,
            Texture2D texture,
            Color color,
            GameConstants.GameLayers layer = GameConstants.GameLayers.Bottom,
            FitTypes fitType = FitTypes.Parent) :
            base(anchorPoints, padding, offset, fitType)
        {
            _backgroundSprite = new DrawableTexture(
                texture == null ? DefaultContent.DefaultRectangle : texture,
                DrawRectangle,
                color,
                layer);
            OnDrawRectangleUpdated += UpdateDestinationRectangle;
        }

        public UI_Panel(
            Vector4 anchorPoints,
            Point4 padding,
            Point offset,
            FitTypes fitType = FitTypes.Parent) :
            base(anchorPoints, padding, offset, fitType)
        {
            _backgroundSprite = null;
            OnDrawRectangleUpdated += UpdateDestinationRectangle;
        }
        #endregion

        #region privates
        private DrawableTexture _backgroundSprite;

        private void UpdateDestinationRectangle()
        {
            if (_backgroundSprite == null) return;

            _backgroundSprite.DestinationRectangle = DrawRectangle;
        }

        protected override void Draw(GameTime gametime, Camera camera)
        {
            if (_backgroundSprite != null)
            {
                camera.DrawTexture(_backgroundSprite);
            }
        }
        #endregion
    }
}
