using DungeonCrawler.Code.UI;
using Microsoft.Xna.Framework;

namespace DungeonCrawler.Code.Entities.Enemies
{
    internal class BasicEnemy : Entity
    {
        // Sprite stuff
        private const string SPRITESHEET_PATH = "Images/Spritesheets/TempEnemy";

        // Pathing / Targeting
        private Entity _pathingTarget;
        private float _timeSinceLastTargetUpdate = 0;
        private float _timeBetweenTargetUpdates = 10; // in milliseconds
        private float _targetingRange = 500;

        public BasicEnemy(Camera camera, EntityManager entityManager)
            : base(camera, entityManager)
        {
            SetupTextures(SPRITESHEET_PATH);

            MoveSpeed = 3;
        }

        private void Pathfind()
        {
            if (_pathingTarget == null) return;

            // Vertical
            if (EntityManager.Player.WorldPosition.Y < WorldPosition.Y) MoveUp();
            else if (EntityManager.Player.WorldPosition.Y > WorldPosition.Y) MoveDown();

            // Horizontal
            if (EntityManager.Player.WorldPosition.X < WorldPosition.X) MoveLeft();
            else if (EntityManager.Player.WorldPosition.X > WorldPosition.X) MoveRight();
        }

        /// <summary>
        /// Set the target to the player if in range, otherwise null
        /// </summary>
        private void UpdateTarget()
        {
            Vector2 playerPosition = EntityManager.Player.WorldPosition;

            // If the player is close
            if (Vector2.Distance(WorldPosition, playerPosition) <= _targetingRange)
            {
                _pathingTarget = EntityManager.Player;
            }
            else
            {
                _pathingTarget = null;
            }

            _timeSinceLastTargetUpdate = 0;
        }

        /// <summary>
        /// Update target if cheks pass
        /// </summary>
        /// <param name="gametime"></param>
        private void TryUpdateTarget(GameTime gametime)
        {
            // Timer check
            if (_timeSinceLastTargetUpdate <= _timeBetweenTargetUpdates) return;

            UpdateTarget();
        }

        protected override void Update(GameTime gametime)
        {
            _timeSinceLastTargetUpdate += gametime.ElapsedGameTime.Milliseconds;
            TryUpdateTarget(gametime);
            Pathfind();

            base.Update(gametime);
        }
    }
}
