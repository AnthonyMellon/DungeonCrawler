using DungeonCrawler.Code.DrawManagement;
using DungeonCrawler.Code.Scenes;
using DungeonCrawler.Code.Utils;
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
                    Position,
                    GameConstants.EntityDirections.FOWARD,
                    GameConstants.GameLayers.Top,
                    _scene);
            }
        }
        public Point Position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
                _sprite.Position = _position;
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
        public int Width => _sprite.Rectangle.Rectangle.Width;
        public int Height => _sprite.Rectangle.Rectangle.Height;

        public void Move(Point moveVector)
        {
            Position += new Point(
                moveVector.X * MoveSpeed,
                moveVector.Y * MoveSpeed);
        }

        public void SetSpriteName(string name)
        {
            _sprite.CurrentSpriteName = name;
        }

        public void SetSpriteColor(Color color)
        {
            _sprite.Color = color;
        }

        public Entity(EntityManager entityManager, Scene scene, bool enabled = true) :
            base(enabled)
        {
            _scene = scene;
            EntityManager = entityManager;
        }
        #endregion

        #region privates        
        private SpriteSheet _spriteSheet;
        private DrawableSprite _sprite;
        private Scene _scene;
        private Point _position;
        #endregion
    }
}
