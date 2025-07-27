using DungeonCrawler.Code.UI.Utils;
using DungeonCrawler.Code.Utils;
using Microsoft.Xna.Framework;

namespace DungeonCrawler.Code.UI
{
    internal class UIComponent : Dynamic
    {
        #region publics

        public AnchorPoints MyAnchorPoints
        { 
            get
            {
                return _myAnchorPoints;
            }
            set
            {
                _myAnchorPoints = value;
                UpdateDrawRectangle();
            }
        }

        public Padding MyPadding
        {
            get
            {
                return _myPadding;
            }
            set
            {
                _myPadding = value;
                UpdateDrawRectangle();
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
                UpdateDrawRectangle();
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
                UpdateDrawRectangle();
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

        protected DrawManager.DrawTargets DrawTarget;

        public UIComponent(
            AnchorPoints anchorPoints,
            Padding padding,
            Point offset,
            DrawManager.DrawTargets drawTarget = DrawManager.DrawTargets.None,
            FitTypes fitType = FitTypes.Parent,
            bool enabled = true) :
            base(enabled)
        {
            AllowScreenRectangleUpdates = false;

            MyAnchorPoints = anchorPoints;
            MyPadding = padding;
            Offset = offset;
            FitType = fitType;
            DrawTarget = drawTarget;

            AllowScreenRectangleUpdates = true;
        }
        #endregion

        #region privates
        private AnchorPoints _myAnchorPoints = AnchorPoints.Center;
        private Padding _myPadding = Padding.Zero;
        private Point _offset = new Point(0, 0);
        private bool _autoUpdateScreenRectangle = true;        

        // Potential optimisation here to only update the draw rectangle here if the fit mode is parent
        // but I dont think it's worth worrying about (I can't be bothered doing it)
        protected override void OnParentSet(Dynamic oldParent, Dynamic newParent)
        {
            UIComponent olduiParent = oldParent as UIComponent;
            if (olduiParent != null) olduiParent.OnDrawRectangleUpdated -= UpdateDrawRectangle;

            UIComponent newUIParent = newParent as UIComponent;
            if (newUIParent != null) newUIParent.OnDrawRectangleUpdated += UpdateDrawRectangle;

            UpdateDrawRectangle();
        }

        // Same optimisation deal as above
        private void OnScreenSizeChange(int width, int height)
        {
            UpdateDrawRectangle();
        }

        private void UpdateDrawRectangle()
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

            newRectangle.Width = newRectangle.Width + MyPadding.X + MyPadding.Y;
            newRectangle.Height = newRectangle.Height + MyPadding.Z + MyPadding.W;

            newRectangle.X = newRectangle.X - MyPadding.X + Offset.X;
            newRectangle.Y = newRectangle.Y - MyPadding.Z + Offset.Y;

            DrawRectangle = newRectangle;
            OnDrawRectangleUpdated?.Invoke();
        }

        private void FitToRectangle(Rectangle parentRectangle)
        {
            Rectangle newRectangle = Rectangle.Empty;

            newRectangle.Width =
                (int)(parentRectangle.Width * MyAnchorPoints.Y) -
                (int)(parentRectangle.Width * MyAnchorPoints.X);
            newRectangle.Width = newRectangle.Width + MyPadding.X + MyPadding.Y;

            newRectangle.Height =
                (int)(parentRectangle.Height * MyAnchorPoints.W) -
                (int)(parentRectangle.Height * MyAnchorPoints.Z);
            newRectangle.Height = newRectangle.Height + MyPadding.Z + MyPadding.W;

            newRectangle.X = parentRectangle.X + (int)(parentRectangle.Width * MyAnchorPoints.X);
            newRectangle.X = newRectangle.X - MyPadding.X + Offset.X;

            newRectangle.Y = parentRectangle.Y + (int)(parentRectangle.Height * MyAnchorPoints.Z);
            newRectangle.Y = newRectangle.Y - MyPadding.Z + Offset.Y;


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
