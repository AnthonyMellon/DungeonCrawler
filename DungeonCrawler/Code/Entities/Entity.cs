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

        public int MoveSpeed { get; set; } = 10;
        public EntityManager EntityManager { get; private set; }
        public int Width => _sprite.Rectangle.Rectangle.Width;
        public int Height => _sprite.Rectangle.Rectangle.Height;

        public void Move(Point moveVector)
        {
            Vector2 normalisedFloatVector = Vector2.Normalize(new Vector2(moveVector.X, moveVector.Y));
            int moveX = float.IsNaN(normalisedFloatVector.X) ? 0 : (int)(normalisedFloatVector.X * MoveSpeed);
            int moveY = float.IsNaN(normalisedFloatVector.Y) ? 0 : (int)(normalisedFloatVector.Y * MoveSpeed);

            Point normalisedMoveVector = new Point(moveX, moveY);

            Position += normalisedMoveVector;
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
