using DungeonCrawler.Code.Utils;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace DungeonCrawler.Code.UI.Utils
{
    internal class DynamicRectangle
    {
        public delegate void RectangleUpdatedHandler();
        public RectangleUpdatedHandler OnRectangleUpdated;

        public enum FitTypes
        {
            Parent,
            Screen,
            None
        }

        public enum GrowFroms
        {
            Center,
            Top,
            Bottom,
            Left,
            Right,
            TopLeft,
            TopRight,
            BottomLeft,
            BottomRight,
            Edges,
            TopStretch,
            BottomStretch,
            LeftStretch,
            RightStretch,
            Auto
        }

        public Rectangle Rectangle { get; private set; }
        public Point ScreenLocation => Rectangle.Location;
        public Point ScreenSize => Rectangle.Size;

        public AnchorPoints AnchorPoints
        {
            get
            {
                return _anchorPoints;
            }
            set
            {
                _anchorPoints = value;
                UpdateRectangle();
            }
        }

        public Size Size
        {
            get
            {
                return _size;
            }
            set
            {
                _size = value;
                UpdateRectangle();
            }
        }

        public Offset Offset
        {
            get
            {
                return _offset;
            }
            set
            {
                _offset = value;
                UpdateRectangle();
            }
        }

        public FitTypes FitType
        {
            get
            {
                return _fitType;
            }
            set
            {
                _fitType = value;
                UpdateRectangle();
            }
        }

        public GrowFroms GrowFrom
        {
            get
            {
                return _growFrom;
            }
            set
            {
                _growFrom = value;
                UpdateRectangle();
            }
        }

        public bool AllowRectangleUpdates
        {
            get
            {
                return _allowRectangleUpdates;
            }
            set
            {
                _allowRectangleUpdates = value;
                if (_allowRectangleUpdates) UpdateRectangle();
            }
        }

        public struct Arguments
        {
            public AnchorPoints AnchorPoints;
            public Size Size;
            public Offset Offset;
            public FitTypes FitType;
            public GrowFroms GrowFrom;
            public DynamicRectangle Parent;

            public Arguments(AnchorPoints anchorPoints, Size size, Offset offset, FitTypes fitType, GrowFroms growfrom, DynamicRectangle parent)
            {
                AnchorPoints = anchorPoints;
                Size = size;
                Offset = offset;
                FitType = fitType;
                GrowFrom = growfrom;
                Parent = parent;
            }
        }

        public DynamicRectangle ParentRectangle
        {
            get
            {
                return _parentRectangle;
            }
            set
            {
                if (_parentRectangle != null) _parentRectangle.OnRectangleUpdated -= UpdateRectangle;
                _parentRectangle = value;
                if (_parentRectangle != null) _parentRectangle.OnRectangleUpdated += UpdateRectangle;

                if (FitType == FitTypes.Parent) UpdateRectangle();
            }
        }

        public DynamicRectangle(
            AnchorPoints anchorPoints,
            Size size,
            Offset offset,
            FitTypes fitType,
            GrowFroms growFrom,
            DynamicRectangle parent)
        {
            Create(new Arguments(
                anchorPoints,
                size,
                offset,
                fitType,
                growFrom,
                parent
                ));
        }

        public DynamicRectangle(Arguments args)
        {
            Create(args);
        }

        public DynamicRectangle(Rectangle rectangle)
        {
            AllowRectangleUpdates = false;
            Rectangle = rectangle;
        }

        private DynamicRectangle _parentRectangle;

        private AnchorPoints _anchorPoints;
        private Size _size;
        private Offset _offset;
        private FitTypes _fitType;
        private GrowFroms _growFrom;

        private bool _allowRectangleUpdates;

        private Dictionary<Vector4, GrowFroms> AnchorVectorToGrowFrom = new Dictionary<Vector4, GrowFroms>
        {
            { AnchorPoints.TopStretch.AsVector, GrowFroms.TopStretch },
            { AnchorPoints.BottomStretch.AsVector, GrowFroms.BottomStretch },
            { AnchorPoints.LeftStretch.AsVector, GrowFroms.LeftStretch },
            { AnchorPoints.RightStretch.AsVector, GrowFroms.RightStretch },
            { AnchorPoints.Center.AsVector, GrowFroms.Center },
            { AnchorPoints.CenterTop.AsVector, GrowFroms.Top },
            { AnchorPoints.CenterBottom.AsVector, GrowFroms.Bottom },
            { AnchorPoints.CenterLeft.AsVector, GrowFroms.Left },
            { AnchorPoints.CenterRight.AsVector, GrowFroms.Right },
            { AnchorPoints.TopLeft.AsVector, GrowFroms.TopLeft },
            { AnchorPoints.TopRight.AsVector, GrowFroms.TopRight },
            { AnchorPoints.BottomLeft.AsVector, GrowFroms.BottomLeft },
            { AnchorPoints.BottomRight.AsVector, GrowFroms.BottomRight },
            { AnchorPoints.Fill.AsVector, GrowFroms.Edges },
        };

        private void Create(Arguments args)
        {
            AllowRectangleUpdates = false;

            _anchorPoints = args.AnchorPoints;
            _size = args.Size;
            _offset = args.Offset;
            _fitType = args.FitType;
            _growFrom = args.GrowFrom;
            _parentRectangle = args.Parent;

            AllowRectangleUpdates = true;

            GameEvents.OnScreenSizeChange += OnScreenSizeChange;
        }

        private void OnScreenSizeChange(int width, int height)
        {
            if (FitType == FitTypes.Screen) UpdateRectangle();
        }

        private void UpdateRectangle()
        {
            if (!AllowRectangleUpdates) return;

            switch (FitType)
            {
                case FitTypes.Parent:
                    FitRectangleToParent();
                    break;
                case FitTypes.Screen:
                    FitRectangleToScreen();
                    break;
                case FitTypes.None:
                default:
                    FitRectangleToNone();
                    break;
            }

            OnRectangleUpdated?.Invoke();
        }

        private void FitRectangleToParent()
        {
            if (_parentRectangle == null)
            {
                FitRectangleToNone();
                return;
            }

            Rectangle parentRectangle = _parentRectangle.Rectangle;

            FitToRectangle(parentRectangle);
        }

        private void FitRectangleToScreen()
        {
            Rectangle screenRectangle = new Rectangle(
                Point.Zero,
                GameValues.ScreenSize);

            FitToRectangle(screenRectangle);
        }

        private void FitRectangleToNone()
        {
            Rectangle newRectangle = Rectangle.Empty;

            newRectangle.Width = Size.Width;
            newRectangle.Height = Size.Height;

            newRectangle.X = 0;
            newRectangle.Y = 0;

            Rectangle = newRectangle;
        }

        private void FitToRectangle(Rectangle fitToRectangle)
        {
            GrowFroms growFrom = GrowFrom == GrowFroms.Auto ?
                AnchorVectorToGrowFrom[AnchorPoints.AsVector] :
                GrowFrom;

            // Calculate Position from Anchor Points
            float minX = fitToRectangle.X + fitToRectangle.Width * AnchorPoints.X;
            float maxX = fitToRectangle.X + fitToRectangle.Width * AnchorPoints.Y;
            float minY = fitToRectangle.Y + fitToRectangle.Height * AnchorPoints.Z;
            float maxY = fitToRectangle.Y + fitToRectangle.Height * AnchorPoints.W;
            int posX = (int)((minX + maxX) / 2);
            int posY = (int)((minY + maxY) / 2);

            // Calculate Width / Height (special case for stretches)            
            Point size = Point.Zero;
            switch (growFrom)
            {
                case GrowFroms.Edges:
                    size.X = (int)(maxX - minX) + Size.Width;
                    size.Y = (int)(maxY - minY) + Size.Height;
                    break;
                case GrowFroms.TopStretch:
                    size.X = (int)(maxX - minX) + Size.Width;
                    size.Y = Size.Height;
                    break;
                case GrowFroms.BottomStretch:
                    size.X = (int)(maxX - minX) + Size.Width;
                    size.Y = Size.Height;
                    break;
                case GrowFroms.LeftStretch:
                    size.X = Size.Width;
                    size.Y = (int)(maxY - minY) + Size.Height;
                    break;
                case GrowFroms.RightStretch:
                    size.X = Size.Width;
                    size.Y = (int)(maxY - minY) + Size.Height;
                    break;
                default:
                    size.X = Size.Width;
                    size.Y = Size.Height;
                    break;
            }

            // Calculate origin from GrowFroms
            //Point parentCenter = fitToRectangle.Center;
            Point location = Point.Zero;
            switch (growFrom)
            {
                case GrowFroms.Center:
                case GrowFroms.Edges:
                    location.X = posX - (size.X / 2);
                    location.Y = posY - (size.Y / 2);
                    break;
                case GrowFroms.Top:
                    location.X = posX - (size.X / 2);
                    location.Y = posY;
                    break;
                case GrowFroms.Bottom:
                    location.X = posX - (size.X / 2);
                    location.Y = posY - size.Y;
                    break;
                case GrowFroms.Left:
                    location.X = posX;
                    location.Y = posY - (size.Y / 2);
                    break;
                case GrowFroms.Right:
                    location.X = posX - size.X;
                    location.Y = posY - (size.Y / 2);
                    break;
                case GrowFroms.TopLeft:
                    location.X = posX;
                    location.Y = posY;
                    break;
                case GrowFroms.TopRight:
                    location.X = posX - size.X;
                    location.Y = posY;
                    break;
                case GrowFroms.BottomLeft:
                    location.X = posX;
                    location.Y = posY - size.Y;
                    break;
                case GrowFroms.BottomRight:
                    location.X = posX - size.X;
                    location.Y = posY - size.Y;
                    break;
                case GrowFroms.TopStretch:
                    location.X = posX - (size.X / 2);
                    location.Y = posY;
                    break;
                case GrowFroms.BottomStretch:
                    location.X = posX - (size.X / 2);
                    location.Y = posY - size.Y;
                    break;
                case GrowFroms.LeftStretch:
                    location.X = posX;
                    location.Y = posY - (size.Y / 2);
                    break;
                case GrowFroms.RightStretch:
                    location.X = posX - size.X;
                    location.Y = posY - (size.Y / 2);
                    break;
            }

            // Offset
            location.X += Offset.X;
            location.Y += Offset.Y;

            Rectangle = new Rectangle(
                location,
                size);
        }
    }
}
