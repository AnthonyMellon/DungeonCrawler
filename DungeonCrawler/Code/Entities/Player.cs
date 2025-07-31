using DungeonCrawler.Code.Input;
using DungeonCrawler.Code.Scenes;
using DungeonCrawler.Code.UI;
using DungeonCrawler.Code.Utils;
using DungeonCrawler.Code.Utils.MathExtras;
using Microsoft.Xna.Framework;

namespace DungeonCrawler.Code.Entities
{
    internal class Player : Entity
    {
        public Player(Camera camera, EntityManager entityManager, Scene scene)
            : base(camera, entityManager, scene)
        {
            SpriteSheet = new SpriteSheet(
                GameConstants.PLAYER_SPRITESHEET_PATH,
                GameConstants.PLAYER_SPRITE_RECTANGLES);
            Layer = GameConstants.GameLayers.World_Player;
            SetSpriteColor(Color.LightGreen);
            camera.FollowEntity(this);
        }

        protected override void Update(GameTime gametime)
        {
            CheckMovement();
            base.Update(gametime);
        }
        private void CheckMovement()
        {
            if (!IsEnabled) return;

            Point moveVector = Point.Zero;
            if (InputProvider.IsKeyDown(InputMap.MoveUp)) moveVector += PointExtras.Up;
            if (InputProvider.IsKeyDown(InputMap.MoveDown)) moveVector += PointExtras.Down;
            if (InputProvider.IsKeyDown(InputMap.MoveLeft)) moveVector += PointExtras.Left;
            if (InputProvider.IsKeyDown(InputMap.MoveRight)) moveVector += PointExtras.Right;
            moveVector = new Point(moveVector.X * MoveSpeed, moveVector.Y * MoveSpeed);
            Move(moveVector);
            if (moveVector != Point.Zero) SetSpriteName(GameConstants.PointToDirection(moveVector));
        }
    }
}
