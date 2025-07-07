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
        private float _targetingRange = 100;

        public BasicEnemy(Camera camera, EntityManager entityManager)
            : base(camera, entityManager)
        {
            SetupTextures(SPRITESHEET_PATH);
        }

        private void Pathfind()
        {
            if (_pathingTarget == null) return;
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

        protected override void Update(GameTime gametime)
        {
            if (_timeSinceLastTargetUpdate <= 0) UpdateTarget();

            base.Update(gametime);
        }
    }
}
