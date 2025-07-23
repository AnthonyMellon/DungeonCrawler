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
                    GameConstants.GameLayers.World_Background);
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
                _sprite.SetDestinationRectangle(
                    _camera.WorldPositionToScreenPosition(value)
                    );
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
            _sprite.SetDestinationRectangle(WorldPosition);
        }

        public void SetSpriteName(string name)
        {
            _sprite.CurrentSpriteName = name;
        }

        public void SetSpriteColor(Color color)
        {
            _sprite.Color = color;
        }

        public Entity(Camera camera, EntityManager entityManager, bool enabled = true) :
            base(enabled)
        {
            _camera = camera;
            EntityManager = entityManager;
        }
        #endregion

        #region privates        
        private SpriteSheet _spriteSheet;
        private DrawableSprite _sprite;
        private Camera _camera;
        private Point _worldPosition;

        protected override void Draw(GameTime gametime, Camera camera)
        {
            camera.DrawSprite(_sprite);
        }
        #endregion
    }
}
