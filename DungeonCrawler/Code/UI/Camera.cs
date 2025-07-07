using DungeonCrawler.Code.Utils;
using Microsoft.Xna.Framework;

namespace DungeonCrawler.Code.UI
{
    internal class Camera : UIComponent
    {
        public Point ScreenSize;
        public Vector2 WorldPosition;
        public Point CenterPosition => ScreenRectangle.Center;

        // Zoom Stuff
        public float ZoomLevel = 1;
        public float MinZoomLevel = 0.5f;
        public float MaxZoomLevel = 5;
        public float ZoomSpeed = 1;

        public Camera(Vector4 anchorPoints, Point4 padding, Point offset)
            : base(anchorPoints, padding, offset)
        {

        }

        public Camera()
            : base(new Vector4(0f, 1f, 0f, 1f), new Point4(0, 0, 0, 0), new Point(0, 0))
        {

        }

        /// <summary>
        /// Update the cameras size
        /// </summary>
        /// <param name="width">New width of camera</param>
        /// <param name="height">New height of camera</param>
        public void UpdateSize(int width, int height)
        {
            ScreenSize = new Point(
                width,
                height
                );

            UpdateScreenRectangle();
        }

        protected override void UpdateScreenRectangle()
        {
            Rectangle myRectangle = new Rectangle
                (
                    0,
                    0,
                    ScreenSize.X,
                    ScreenSize.Y
                );
            ScreenRectangle = myRectangle;
            OnScreenRectangleUpdated?.Invoke();
        }

        /// <summary>
        /// Move the camera around the world
        /// </summary>
        /// <param name="pannValue">Vector2 to add to current world position</param>
        public void Pan(Vector2 pannValue)
        {
            WorldPosition += pannValue;
        }

        /// <summary>
        /// Move the camera in and out of the world
        /// </summary>
        /// <param name="direction">Direction to zoom the camera in</param>
        public void Zoom(int direction)
        {
            ZoomLevel += direction * ZoomSpeed;

            // Clamp zoom level
            if (ZoomLevel < MinZoomLevel) ZoomLevel = MinZoomLevel;
            else if (ZoomLevel > MaxZoomLevel) ZoomLevel = MaxZoomLevel;
        }
    }
}
