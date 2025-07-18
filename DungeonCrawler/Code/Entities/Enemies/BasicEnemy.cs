using DungeonCrawler.Code.UI;
using DungeonCrawler.Code.Utils;
using Microsoft.Xna.Framework;

namespace DungeonCrawler.Code.Entities.Enemies
{
    internal class BasicEnemy : Entity
    {        
        // Pathing / Targeting
        private Entity _pathingTarget;
        private float _timeSinceLastTargetUpdate = 0;
        private float _timeBetweenTargetUpdates = 10; // in milliseconds
        private float _targetingRange = 500;

        public BasicEnemy(Camera camera, EntityManager entityManager)
            : base(camera, entityManager)
        {
            SpriteSheet = new SpriteSheet(GameConstants.ENEMY_SPRITESHEET_PATH,
                GameConstants.ENEMY_SPRITE_RECTANGLES);
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
            Point playerPosition = EntityManager.Player.WorldPosition;
                      
            if (GetDistanceToPlayer() <= _targetingRange)
            {
                _pathingTarget = EntityManager.Player;
            }
            else
            {
                _pathingTarget = null;
            }

            _timeSinceLastTargetUpdate = 0;
        }

        private float GetDistanceToPlayer()
        {
            Point playerPosition = EntityManager.Player.WorldPosition;

            return Vector2.Distance(
                new Vector2(WorldPosition.X, WorldPosition.Y),
                new Vector2(playerPosition.X, playerPosition.Y));
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
