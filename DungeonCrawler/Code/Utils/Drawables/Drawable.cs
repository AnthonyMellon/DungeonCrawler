using DungeonCrawler.Code.Scenes;
using DungeonCrawler.Code.UI.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;

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
            set
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
            Scene scene,
            bool visible = true)
        {
            CreateDrawable(layer, scene, visible, null);
        }

        public Drawable(
            GameConstants.GameLayers layer,
            AnchorPoints anchorPoints,
            Size size,
            Offset offset,
            DynamicRectangle.FitTypes fitType,
            DynamicRectangle.GrowFroms growFrom,
            DynamicRectangle parent,
            Scene scene,
            bool visible = true)
        {
            DynamicRectangle.Arguments arguments = new DynamicRectangle.Arguments(
                anchorPoints,
                size,
                offset,
                fitType,
                growFrom,
                parent);

            CreateDrawable(layer, scene, visible, arguments);
        }

        private bool _visible;
        private Scene _scene;
        private DynamicRectangle _destinationRectangle;
        private Color _color;
        private GameConstants.GameLayers _layer;

        private void CreateDrawable(GameConstants.GameLayers layer, Scene scene, bool visible, DynamicRectangle.Arguments? args)
        {
            Debug.WriteLine($"Create Drawable {scene}");

            _layer = layer;
            _scene = scene;
            Visible = visible;

            if (args.HasValue) Rectangle = new DynamicRectangle(args.Value);

            scene?.RegisterDrawable(this);
        }


    }
}
