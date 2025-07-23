using DungeonCrawler.Code.Utils;
using Microsoft.Xna.Framework;

namespace DungeonCrawler.Code.UI
{
    internal class UIComponent : Dynamic
    {
        #region publics       
        public Vector4 AnchorPoints
        {
            get
            {
                return _anchorPoints;
            }
            set
            {
                _anchorPoints = value;
                UpdateScreenRectangle();
            }
        }

        public Point4 Padding
        {
            get
            {
                return _padding;
            }
            set
            {
                _padding = value;
                UpdateScreenRectangle();
            }
        }

        public Point Offset
        {
            get
            {
                return _offset;
            }
            set
            {
                _offset = value;
                UpdateScreenRectangle();
            }
        }

        public bool AllowScreenRectangleUpdates
        {
            get
            {
                return _autoUpdateScreenRectangle;
            }
            set
            {
                _autoUpdateScreenRectangle = value;
                UpdateScreenRectangle();
            }
        }

        public enum FitTypes
        {
            Parent,
            Screen,
            None
        }
        public FitTypes FitType { get; set; }

        public Rectangle DrawRectangle { get; set; }

        public delegate void DrawRectangleUpdatedHandler();
        public DrawRectangleUpdatedHandler OnDrawRectangleUpdated;



        public UIComponent(
            Vector4 anchorPoints,
            Point4 padding,
            Point offset,
            FitTypes fitType = FitTypes.Parent,
            bool enabled = true) :
            base(enabled)
        {
            AllowScreenRectangleUpdates = false;

            AnchorPoints = anchorPoints;
            Padding = padding;
            Offset = offset;
            FitType = fitType;

            AllowScreenRectangleUpdates = true;
        }
        #endregion

        #region privates
        private Vector4 _anchorPoints = new Vector4(0.5f, 0.5f, 0.5f, 0.5f);
        private Point4 _padding = new Point4(0, 0, 0, 0);
        private Point _offset = new Point(0, 0);
        private bool _autoUpdateScreenRectangle = true;

        // Potential optimisation here to only update the screen rectangle here if the fit mode is parent
        // but I dont think it's worth worrying about (I can't be bothered doing it)
        protected override void OnParentSet(Dynamic oldParent, Dynamic newParent)
        {
            UIComponent olduiParent = oldParent as UIComponent;
            if (olduiParent != null) olduiParent.OnDrawRectangleUpdated -= UpdateScreenRectangle;

            UIComponent newUIParent = newParent as UIComponent;
            if (newUIParent != null) newUIParent.OnDrawRectangleUpdated += UpdateScreenRectangle;

            UpdateScreenRectangle();
        }

        // Same optimisation deal as above
        private void OnScreenSizeChange(int width, int height)
        {
            UpdateScreenRectangle();
        }

        private void UpdateScreenRectangle()
        {
            if (!AllowScreenRectangleUpdates || !IsEnabled) return;

            switch (FitType)
            {
                case FitTypes.Parent:
                    FitDawRectangleToParent();
                    break;

                case FitTypes.Screen:
                    FitDrawRectanlgeToScreen();
                    break;
                case FitTypes.None:
                default:
                    FitDrawRectangleToNone();
                    break;
            }
        }

        private void FitDrawRectanlgeToScreen()
        {
            Rectangle screenRectangle = new Rectangle(
                Point.Zero,
                GameValues.ScreenSize
                );

            FitToRectangle(screenRectangle);
        }

        private void FitDawRectangleToParent()
        {
            UIComponent UIParent = Parent as UIComponent;
            if (UIParent == null) return;

            FitToRectangle(UIParent.DrawRectangle);
        }

        private void FitDrawRectangleToNone()
        {
            Rectangle newRectangle = Rectangle.Empty;

            newRectangle.Width = newRectangle.Width + Padding.X + Padding.Y;
            newRectangle.Height = newRectangle.Height + Padding.Z + Padding.W;

            newRectangle.X = newRectangle.X - Padding.X + Offset.X;
            newRectangle.Y = newRectangle.Y - Padding.Z + Offset.Y;

            DrawRectangle = newRectangle;
            OnDrawRectangleUpdated?.Invoke();
        }

        private void FitToRectangle(Rectangle parentRectangle)
        {
            Rectangle newRectangle = Rectangle.Empty;

            newRectangle.Width =
                (int)(parentRectangle.Width * AnchorPoints.Y) -
                (int)(parentRectangle.Width * AnchorPoints.X);
            newRectangle.Width = newRectangle.Width + Padding.X + Padding.Y;

            newRectangle.Height =
                (int)(parentRectangle.Height * AnchorPoints.W) -
                (int)(parentRectangle.Height * AnchorPoints.Z);
            newRectangle.Height = newRectangle.Height + Padding.Z + Padding.W;

            newRectangle.X = parentRectangle.X + (int)(parentRectangle.Width * AnchorPoints.X);
            newRectangle.X = newRectangle.X - Padding.X + Offset.X;

            newRectangle.Y = parentRectangle.Y + (int)(parentRectangle.Height * AnchorPoints.Z);
            newRectangle.Y = newRectangle.Y - Padding.Z + Offset.Y;


            DrawRectangle = newRectangle;
            OnDrawRectangleUpdated?.Invoke();
        }

        protected override void OnEnable()
        {
            GameEvents.OnScreenSizeChange += OnScreenSizeChange;
        }

        protected override void OnDisable()
        {
            GameEvents.OnScreenSizeChange -= OnScreenSizeChange;
        }
        #endregion
    }
}
