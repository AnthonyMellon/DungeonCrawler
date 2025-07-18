using DungeonCrawler.Code.Input;
using DungeonCrawler.Code.UI;
using DungeonCrawler.Code.Utils;
using Microsoft.Xna.Framework;

namespace DungeonCrawler.Code.Entities
{
    internal class Player : Entity
    {
        public Player(Camera camera, EntityManager entityManager)
            : base(camera, entityManager)
        {
            SpriteSheet = new SpriteSheet(
                GameConstants.PLAYER_SPRITESHEET_PATH,
                GameConstants.PLAYER_SPRITE_RECTANGLES);            
            camera.FollowEntity(this);
        }

        protected override void Update(GameTime gametime)
        {
            CheckMovement();
            base.Update(gametime);
        }
        private void CheckMovement()
        {
            if (!IsActive) return;

            if (InputProvider.IsKeyDown(InputMap.MoveUp)) MoveUp();
            if (InputProvider.IsKeyDown(InputMap.MoveDown)) MoveDown();
            if (InputProvider.IsKeyDown(InputMap.MoveLeft)) MoveLeft();
            if (InputProvider.IsKeyDown(InputMap.MoveRight)) MoveRight();
        }
    }
}
