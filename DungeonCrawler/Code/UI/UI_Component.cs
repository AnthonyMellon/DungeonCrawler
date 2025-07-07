using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DungeonCrawler.Code.Utils;

namespace DungeonCrawler.Code.UI
{
    internal class UIComponent : Dynamic
    {
        /// <summary>
        /// The components 4 points relative to the parent object from 0-1 (X1, X2, Y1, Y2)
        /// </summary>
        public Vector4 AnchorPoints
        {
            get
            {
                return b_anchorPoints;
            }
            set
            {
                b_anchorPoints = value;
                UpdateScreenRectangleAuto();
            }
        }
        private Vector4 b_anchorPoints = new Vector4(0.5f, 0.5f, 0.5f, 0.5f);

        /// <summary>
        /// Extra sizing beyond anchors (in pixels)
        /// </summary>
        public Point4 Padding
        {
            get
            {
                return b_padding;
            }
            set
            {
                b_padding = value;
                UpdateScreenRectangleAuto();
            }
        }
        private Point4 b_padding = new Point4(0, 0, 0, 0);

        /// <summary>
        /// Offset rectangle made by <see cref="Padding"/>
        /// </summary>
        public Point Offset
        {
            get
            {
                return b_offset;
            }
            set
            {
                b_offset = value;
                UpdateScreenRectangleAuto();
            }
        }
        private Point b_offset = new Point(0, 0);

        /// <summary>
        /// The final computed rectangle used for drawing
        /// </summary>
        public Rectangle ScreenRectangle;

        /// <summary>
        /// Should the screen rectangle be updated when sizes are changed?
        /// Setting to true will update the screen rectangle
        /// </summary>
        public bool AutoUpdateScreenRectangle
        {
            get
            {
                return b_autoUpdateScreenRectangle;
            }
            set
            {
                b_autoUpdateScreenRectangle = value;
                UpdateScreenRectangleAuto();
            }
        }
        private bool b_autoUpdateScreenRectangle = true;

        public delegate void ScreenRectangleUpdatedHandler();
        public ScreenRectangleUpdatedHandler OnScreenRectangleUpdated;

        public UIComponent(Vector4 anchorPoints, Point4 padding, Point offset)
        {
            AutoUpdateScreenRectangle = false;

            AnchorPoints = anchorPoints;
            Padding = padding;
            Offset = offset;

            AutoUpdateScreenRectangle = true;
        }

        /// <summary>
        /// Called when the parent is set
        /// </summary>
        /// <param name="oldParent">The old parent</param>
        /// <param name="newParent">The new parent</param>
        protected override void OnParentSet(Dynamic oldParent, Dynamic newParent)
        {
            // Stop listening to old parents rectangle updates
            if (oldParent != null && oldParent.GetType().IsSubclassOf(typeof(UIComponent)))
            {
                UIComponent uiParent = (UIComponent)oldParent;
                uiParent.OnScreenRectangleUpdated -= UpdateScreenRectangleAuto;
            }

            // Start listening to new parents rectangle updates
            if (newParent != null && newParent.GetType().IsSubclassOf(typeof(UIComponent)))
            {
                UIComponent uiParent = (UIComponent)newParent;
                uiParent.OnScreenRectangleUpdated += UpdateScreenRectangleAuto;
            }

            // Update the screen rectangle
            UpdateScreenRectangleAuto();
        }

        private void UpdateScreenRectangleAuto()
        {
            //Debug.WriteLine($"Update {AutoUpdateScreenRectangle}");

            if (!AutoUpdateScreenRectangle) return;
            UpdateScreenRectangle();
        }

        /// <summary>
        /// Updates <see cref="ScreenRectangle"/> using Anchors (if parent is a <see cref="UIComponent"/>, Padding, and Offset
        /// </summary>
        protected virtual void UpdateScreenRectangle()
        {
            Rectangle myRectangle = new Rectangle(0, 0, 0, 0);

            // Parent is UIComponent so anchor points need to be used
            if (Parent != null && Parent.GetType().IsSubclassOf(typeof(UIComponent)))
            {
                UIComponent UIParent = (UIComponent)Parent;
                Rectangle parentRect = UIParent.ScreenRectangle;

                Rectangle anchordRect = new Rectangle(0, 0, 0, 0);

                // Calculate width \ height
                anchordRect.Width =
                    (int)(parentRect.Width * AnchorPoints.Y) -
                    (int)(parentRect.Width * AnchorPoints.X);

                anchordRect.Height =
                    (int)(parentRect.Height * AnchorPoints.W) -
                    (int)(parentRect.Height * AnchorPoints.Z);

                // Calculate Position
                anchordRect.X = parentRect.X + (int)(parentRect.Width * AnchorPoints.X);
                anchordRect.Y = parentRect.Y + (int)(parentRect.Height * AnchorPoints.Z);

                // Apply
                myRectangle = anchordRect;
            }

            // Calculate width / height
            myRectangle.Width = myRectangle.Width + Padding.X + Padding.Y;
            myRectangle.Height = myRectangle.Height + Padding.Z + Padding.W;

            // Calculate position
            myRectangle.X = myRectangle.X - Padding.X + Offset.X;
            myRectangle.Y = myRectangle.Y - Padding.Z + Offset.Y;

            // Apply
            ScreenRectangle = myRectangle;
            OnScreenRectangleUpdated?.Invoke();
        }

        protected override void Update(GameTime gametime) { }

        protected override void Draw(GameTime gametime, SpriteBatch graphics) { }
    }
}
