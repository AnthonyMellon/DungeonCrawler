using DungeonCrawler.Code.Scenes;
using DungeonCrawler.Code.UI.Utils;
using DungeonCrawler.Code.Utils.Drawables;

namespace DungeonCrawler.Code.UI
{
    internal class UIComponent : Dynamic
    {
        #region publics
        public DynamicRectangle Rectangle;
        protected Scene Scene;
        protected Drawable DrawTexture;

        public UIComponent(
            DynamicRectangle rectangle,
            Scene scene,
            bool enabled = true) :
            base(enabled)
        {
            Rectangle = rectangle;
            Scene = scene;
            Rectangle.OnRectangleUpdated += UpdateTextureRectangle;
        }

        public UIComponent(
            AnchorPoints anchorPoints,
            Size size,
            Offset offset,
            DynamicRectangle.FitTypes fitType,
            DynamicRectangle.GrowFroms growFrom,
            Scene scene,
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
            Scene = scene;
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
            if (Rectangle != null) Rectangle.AllowRectangleUpdates = true;

        }

        protected override void OnDisable()
        {
            if (DrawTexture != null) DrawTexture.Visible = false;
            if (Rectangle != null) Rectangle.AllowRectangleUpdates = false;
        }
        #endregion
    }
}
