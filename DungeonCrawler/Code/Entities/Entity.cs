using DungeonCrawler.Code.Scenes;
using DungeonCrawler.Code.UI;
using DungeonCrawler.Code.Utils;
using DungeonCrawler.Code.Utils.Drawables;
using Microsoft.Xna.Framework;

namespace DungeonCrawler.Code.Entities
{
    internal class Entity : Dynamic
    {
        #region publics       
        public SpriteSheet SpriteSheet
        {
            get
            {
                return _spriteSheet;
            }
            set
            {
                _spriteSheet = value;
                _sprite = new DrawableSprite(
                    SpriteSheet,
                    _camera.WorldPositionToScreenPosition(WorldPosition),
                    GameConstants.EntityDirections.FOWARD,
                    GameConstants.GameLayers.Top,
                    _scene);
            }
        }
        public Point WorldPosition
        {
            get
            {
                return _worldPosition;
            }
            set
            {
                _worldPosition = value;
                _sprite.Position = _camera.WorldPositionToScreenPosition(value);
            }
        }
        public GameConstants.GameLayers Layer
        {
            get
            {
                return _sprite.Layer;
            }
            set
            {
                _sprite.Layer = value;
            }
        }

        public int MoveSpeed { get; set; } = 5;
        public EntityManager EntityManager { get; private set; }

        public void Move(Point moveVector)
        {
            WorldPosition += moveVector;
            _sprite.Position = _camera.WorldPositionToScreenPosition(_worldPosition);
        }

        public void SetSpriteName(string name)
        {
            _sprite.CurrentSpriteName = name;
        }

        public void SetSpriteColor(Color color)
        {
            _sprite.Color = color;
        }

        public Entity(Camera camera, EntityManager entityManager, Scene scene, bool enabled = true) :
            base(enabled)
        {
            _camera = camera;
            _scene = scene;
            EntityManager = entityManager;
        }
        #endregion

        #region privates        
        private SpriteSheet _spriteSheet;
        private DrawableSprite _sprite;
        private Camera _camera;
        private Point _worldPosition;
        private Scene _scene;
        #endregion
    }
}
