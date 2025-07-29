using DungeonCrawler.Code.DrawManagement;
using DungeonCrawler.Code.UI.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace DungeonCrawler.Code.Utils.Drawables
{
    internal abstract class Drawable
    {
        public Action OnChange;
        public DynamicRectangle Rectangle
        {
            get
            {
                return _destinationRectangle;
            }
            private set
            {
                _destinationRectangle = value;
                OnChange?.Invoke();
            }
        }
        public Color Color 
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
                OnChange?.Invoke();
            }
        }
        public Point Position
        {
            get
            {
                return Rectangle.ScreenLocation;
            }
            set
            {
                Rectangle newRectangle = new Rectangle(
                    value,
                    Rectangle == null ? new Point(0, 0) : Rectangle.ScreenSize
                    );
                Rectangle = new DynamicRectangle(newRectangle);
            }
        }
        public Point Size
        {
            get
            {
                return Rectangle.ScreenSize;
            }
            set
            {
                Rectangle newRectangle = new Rectangle(
                    Rectangle == null ? new Point(0, 0) : Rectangle.ScreenLocation,
                    value
                    );
                Rectangle = new DynamicRectangle(newRectangle);
            }
        }
        public bool Visible
        {
            get
            {
                return _visible;
            }
            set
            {
                _visible = value;

                if (_visible) DrawManager.RegisterDrawable(_drawTarget, this);
                if (!_visible) DrawManager.DeregisterDrawable(_drawTarget, this);
                
                OnChange?.Invoke();
                OnVisabilitySet?.Invoke(_visible);
            }
        }
        public GameConstants.GameLayers Layer
        {
            get
            {
                return _layer;
            }
            set
            {
                _layer = value;
                OnChange?.Invoke();
            }
        }
        public Action<bool> OnVisabilitySet;

        public abstract void Draw(SpriteBatch spritebatch);
        public abstract void Draw(SpriteBatch spritebatch, Rectangle destinationRectangle);

        public Drawable(
            GameConstants.GameLayers layer,
            DrawManager.DrawTargets drawTarget = DrawManager.DrawTargets.None,
            bool visible = true)
        {
            _layer = layer;
            _drawTarget = drawTarget;
            Visible = visible;
        }

        private bool _visible;
        private DrawManager.DrawTargets _drawTarget;
        private DynamicRectangle _destinationRectangle;
        private Color _color;
        private GameConstants.GameLayers _layer;
    }
}
