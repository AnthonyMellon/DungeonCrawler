using DungeonCrawler.Code.UI;
using DungeonCrawler.Code.Utils;
using DungeonCrawler.Code.Utils.Drawables;
using Microsoft.Xna.Framework;

namespace DungeonCrawler.Code.Entities
{
    internal class Entity : Dynamic
    {
        #region publics       
        public SpriteSheet SpriteSheet { get; set; }
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
        public int MoveSpeed { get; set; } = 5;
        public EntityManager EntityManager { get; private set; }

        public void MoveUp()
        {
            Move(new Point(0, -1 * MoveSpeed));
            _sprite.CurrentSpriteName = GameConstants.EntityDirections.Back;

        }
        public void MoveDown()
        {
            Move(new Point(0, MoveSpeed));
            _sprite.CurrentSpriteName = GameConstants.EntityDirections.foward;
        }
        public void MoveLeft()
        {
            Move(new Point(-1 * MoveSpeed, 0));
            _sprite.CurrentSpriteName = GameConstants.EntityDirections.Left;
        }
        public void MoveRight()
        {
            Move(new Point(MoveSpeed, 0));
            _sprite.CurrentSpriteName = GameConstants.EntityDirections.Right;
        }

        public Entity(Camera camera, EntityManager entityManager)
        {
            _camera = camera;
            EntityManager = entityManager;

            _sprite = new DrawableSprite(
                SpriteSheet,
                camera.WorldPositionToScreenPosition(WorldPosition),
                GameConstants.EntityDirections.foward,
                GameConstants.GameLayers.World);
        }
        #endregion

        #region privates        
        private DrawableSprite _sprite;
        private Camera _camera;
        private Point _worldPosition;

        private void Move(Point moveVector)
        {
            WorldPosition += moveVector;
            _sprite.SetDestinationRectangle(WorldPosition);
        }

        protected override void Draw(GameTime gametime, Camera camera)
        {
            camera.DrawSprite(_sprite);
        }

        protected override void Update(GameTime gametime) { }
        #endregion
    }
}
