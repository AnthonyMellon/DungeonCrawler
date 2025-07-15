using DungeonCrawler.Code.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DungeonCrawler.Code.Utils;
using DungeonCrawler.Code.Utils.Drawables;

namespace DungeonCrawler.Code.UI
{
    internal class Camera
    {
        #region Publics

        public Point ScreenSize { get; private set; }
        public Vector2 WorldPosition
        {
            get
            {
                return _trackedEntity == null ?
                    b_worldPosition :
                    _trackedEntity.WorldPosition + _trackedEntityOffset;
            }
            set
            {
                if (_trackedEntity == null)
                {
                    b_worldPosition = value;
                }
            }
        }
        public Rectangle CameraRectangle { get; private set; }
        public Point CenterPosition => CameraRectangle.Center;

        public OnCameraSizeChangeHandler OnCameraSizeChange { get; set; }
        public delegate void OnCameraSizeChangeHandler(Rectangle cameraRectangle);

        // Zoom Stuff
        public float ZoomLevel = 1;
        public float MinZoomLevel = 0.5f;
        public float MaxZoomLevel = 5;
        public float ZoomSpeed = 1;

        public Camera(SpriteBatch graphics)
        {
            _graphics = graphics;
        }

        public void DrawImage(Texture2D texture, Vector2 position, Rectangle sourceRectangle, GameConstants.GameLayers layer)
        {
            _graphics.Draw(
                texture,
                position,
                sourceRectangle,
                Color.White,
                0,
                new Vector2(0, 0),
                1,
                SpriteEffects.None,
                GameConstants.GameLayerToLayer(layer)
                );
        }
    
        public void DrawImageToWorld(IWorldDrawable drawable)
        {
            _graphics.Draw(
                drawable.Texture,
                drawable.Position,
                drawable.SourceRectangle,
                drawable.Color,
                drawable.Rotation,
                drawable.Origin,
                drawable.Scale,
                SpriteEffects.None,
                GameConstants.GameLayerToLayer(drawable.Layer)
                );
        }

        public void DrawImageToScreen(IScreenDrawable drawable)
        {
            _graphics.Draw(
                drawable.Texture,
                drawable.DestinationRectangle,
                drawable.SourceRectangle,
                drawable.Color,
                drawable.Rotation,
                drawable.Origin,
                SpriteEffects.None,
                GameConstants.GameLayerToLayer(drawable.Layer)
                );
        }

        public void DrawText(IDrawableText drawableText)
        {
            _graphics.DrawString(
                drawableText.Font,
                drawableText.Text,
                drawableText.Position,
                drawableText.Color,
                drawableText.Rotation,
                drawableText.Origin,
                drawableText.Scale,
                SpriteEffects.None,
                GameConstants.GameLayerToLayer(drawableText.Layer)
                );
        }

        public void UpdateSize(int width, int height)
        {
            ScreenSize = new Point(
                width,
                height
                );

            UpdateCameraRectangle();
        }

        public void FollowEntity(Entity entity)
        {
            FollowEntity(entity, Vector2.Zero);
        }
        public void FollowEntity(Entity entity, Vector2 offset)
        {
            _trackedEntity = entity;
            _trackedEntityOffset = offset;
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

        #endregion

        #region Privates

        private Entity _trackedEntity;
        private Vector2 _trackedEntityOffset = Vector2.Zero;
        private Vector2 b_worldPosition;
        private SpriteBatch _graphics;

        private void UpdateCameraRectangle()
        {
            Rectangle newRectangle = new Rectangle
                (
                0,
                0,
                ScreenSize.X,
                ScreenSize.Y
                );

            CameraRectangle = newRectangle;
        }

        #endregion
    }
}
