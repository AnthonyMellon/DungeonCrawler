using DungeonCrawler.Code.DrawManagement;
using DungeonCrawler.Code.UI.Utils;
using DungeonCrawler.Code.Utils;
using DungeonCrawler.Code.Utils.Drawables;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace DungeonCrawler.Code.UI
{
    internal class UIComponent : Dynamic
    {
        #region publics
        public DynamicRectangle Rectangle;
        protected DrawManager.DrawTargets DrawTarget;
        protected Drawable DrawTexture;

        public UIComponent(
            DynamicRectangle rectangle,
            DrawManager.DrawTargets drawTarget,
            bool enabled = true) :
            base (enabled)
        {
            Rectangle = rectangle;
            DrawTarget = drawTarget;
            Rectangle.OnRectangleUpdated += UpdateTextureRectangle;
        }

        public UIComponent(
            AnchorPoints anchorPoints,
            Size size,
            Offset offset,
            DynamicRectangle.FitTypes fitType,
            DynamicRectangle.GrowFroms growFrom,
            DrawManager.DrawTargets drawTarget,
            bool enabled = true) :
            base(enabled)
        {
            Rectangle = new DynamicRectangle(
                anchorPoints,
                size,
                offset,
                fitType,
                growFrom,
                null);
            DrawTarget = drawTarget;
            Rectangle.OnRectangleUpdated += UpdateTextureRectangle;
        }
        #endregion

        #region privates        
        protected override void OnParentSet(Dynamic oldParent, Dynamic newParent)
        {
            UIComponent uiParent = newParent as UIComponent;
            if (uiParent != null)
            {                
                Rectangle.ParentRectangle = uiParent.Rectangle;
            }
            else
            {
                Rectangle.ParentRectangle = null;
            }
        }

        private void UpdateTextureRectangle()
        {
            if (DrawTexture == null) return;

            DrawTexture.Position = Rectangle.ScreenLocation;
            DrawTexture.Size = Rectangle.ScreenSize;
        }

        protected override void OnEnable()
        {
            if (DrawTexture != null) DrawTexture.Visible = true;
            if(Rectangle != null) Rectangle.AllowRectangleUpdates = true;

        }

        protected override void OnDisable()
        {
            if (DrawTexture != null) DrawTexture.Visible = false;
            if (Rectangle != null) Rectangle.AllowRectangleUpdates = false;
        }
        #endregion
    }
}
