using DungeonCrawler.Code.Input;
using DungeonCrawler.Code.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonCrawler.Code.Entities
{
    internal class Player : Entity
    {
        // Sprite stuff
        private const string SPRITESHEET_PATH = "Images/Spritesheets/TempCharacter";

        public Player(Camera camera, EntityManager entityManager)
            : base(camera, entityManager)
        {
            SetupTextures(SPRITESHEET_PATH);
        }

        protected override void Update(GameTime gametime)
        {
            CheckMovement();
            CenterCameraOnPosition();

            base.Update(gametime);
        }

        protected override void Draw(GameTime gametime, SpriteBatch graphics)
        {
            base.Draw(gametime, graphics);
        }

        /// <summary>
        /// Listen to inputs and move accordingly
        /// </summary>
        private void CheckMovement()
        {
            // Don't move if not active
            if (!IsActive) return;

            if (InputProvider.IsKeyDown(InputMap.MoveUp)) MoveUp();
            if (InputProvider.IsKeyDown(InputMap.MoveDown)) MoveDown();
            if (InputProvider.IsKeyDown(InputMap.MoveLeft)) MoveLeft();
            if (InputProvider.IsKeyDown(InputMap.MoveRight)) MoveRight();
        }

        /// <summary>
        /// Center the camera on the player
        /// </summary>
        private void CenterCameraOnPosition()
        {
            Camera.WorldPosition = WorldPosition;
        }
    }
}
