using DungeonCrawler.Code.Entities;
using DungeonCrawler.Code.Utils;
using Microsoft.Xna.Framework;

namespace DungeonCrawler.Code.UI
{
    internal class Camera
    {
        #region Publics

        public Matrix Transform { get; private set; }

        public Matrix TransformToEntity(Entity entity)
        {
            return TransformToEntity(entity, Point.Zero);
        }

        public Matrix TransformToEntity(Entity entity, Point entityOffset)
        {
            Matrix position = Matrix.CreateTranslation(
                -(entity.Position.X + entityOffset.X) - (entity.Width / 2),
                -(entity.Position.Y + entityOffset.Y) - (entity.Height / 2),
                0);

            Matrix scale = Matrix.CreateScale(
                _zoomLevel,
                _zoomLevel,
                0);

            Matrix screenOffset = Matrix.CreateTranslation(
                GameValues.ScreenSize.X / 2,
                GameValues.ScreenSize.Y / 2,
                0);

            Transform = position * scale * screenOffset;
            return Transform;
        }

        public void Zoom(int direction)
        {
            _zoomLevel += direction * _zoomSpeed;
            
            if (_zoomLevel < _minZoomLevel) _zoomLevel = _minZoomLevel;
            else if (_zoomLevel > _maxZoomLevel) _zoomLevel = _maxZoomLevel;
        }
        #endregion

        #region Privates        
        private float _zoomLevel = 0.2f;
        private float _minZoomLevel = 0.15f;
        private float _maxZoomLevel = 1.5f;
        private float _zoomSpeed = 0.05f;
        #endregion
    }
}
