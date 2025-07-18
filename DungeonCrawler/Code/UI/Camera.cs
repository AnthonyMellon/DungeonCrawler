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
        public Point WorldPosition
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
    
        public void DrawSprite(DrawableSprite sprite)
        {
            _graphics.Draw(
                sprite.SpriteSheet.Sheet,
                sprite.DestinationRectangle,
                sprite.SourceRectangle,
                sprite.Color,
                0,              // Rotation
                Vector2.Zero,   // Origin       currently no use for these values  
                SpriteEffects.None,
                GameConstants.GameLayerToLayer(sprite.Layer)
                );
        }


        public void DrawTexture(DrawableTexture texture)
        {
            _graphics.Draw
                (
                texture.Texture,
                texture.DestinationRectangle,
                texture.Texture.Bounds,
                texture.Color,
                0,              // Rotation
                Vector2.Zero,   // Origin       currently no use for these values
                SpriteEffects.None,
                GameConstants.GameLayerToLayer(texture.Layer)
                );
        }
        public void DrawText(DrawableText text)
        {            
            _graphics.DrawString(
                text.Font,
                text.Text,
                text.Position,
                text.Color,
                0,              // Rotation
                Vector2.Zero,   // Origin
                1,              // Scale        currently no use for these values
                SpriteEffects.None,
                GameConstants.GameLayerToLayer(text.Layer)
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
            FollowEntity(entity, Point.Zero);
        }
        public void FollowEntity(Entity entity, Point offset)
        {
            _trackedEntity = entity;
            _trackedEntityOffset = offset;
        }

        /// <summary>
        /// Move the camera around the world
        /// </summary>
        /// <param name="pannValue">Vector2 to add to current world position</param>
        public void Pan(Point pannValue)
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

        public Point WorldPositionToScreenPosition(Point worldPosition)
        {
            Point screenPosition = (worldPosition - WorldPosition) + CenterPosition;
            return screenPosition;
        }

        #endregion

        #region Privates

        private Entity _trackedEntity;
        private Point _trackedEntityOffset = Point.Zero;
        private Point b_worldPosition;
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
