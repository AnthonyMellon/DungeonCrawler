using DungeonCrawler.Code.Entities.Pathing;
using DungeonCrawler.Code.Entities.Pathing.TargetFinders;
using DungeonCrawler.Code.UI;
using DungeonCrawler.Code.Utils;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace DungeonCrawler.Code.Entities.Enemies
{
    internal class BasicEnemy : Entity
    {
        #region publics
        public BasicEnemy(Camera camera, EntityManager entityManager)
            : base(camera, entityManager)
        {
            SpriteSheet = new SpriteSheet(
                GameConstants.ENEMY_SPRITESHEET_PATH,
                GameConstants.ENEMY_SPRITE_RECTANGLES);
            Layer = GameConstants.GameLayers.World_Enemies;
            SetSpriteColor(Color.Red);

            _targetFinder = new FirstInRange(
                _targetingRange,
                new List<Entity> { EntityManager.Player }
                );

            _pathFinder = new PathFinder();

            MoveSpeed = 3;
        }
        #endregion

        #region privates
        private Entity _pathingTarget;
        private float _millisecondsSinceLastTargetUpdate = 0;
        private float _millisecondsBetweenTargetUpdates = 10;
        private float _targetingRange = 500;
        private PathFinder _pathFinder;
        private ITargetFinder _targetFinder;

        private void TryUpdateTarget(GameTime gametime)
        {
            if (_millisecondsSinceLastTargetUpdate <= _millisecondsBetweenTargetUpdates) return;

            _pathingTarget = _targetFinder.FindTarget(WorldPosition);
            _millisecondsSinceLastTargetUpdate = 0;
        }

        protected override void Update(GameTime gametime)
        {
            _millisecondsSinceLastTargetUpdate += gametime.ElapsedGameTime.Milliseconds;
            TryUpdateTarget(gametime);

            _pathFinder.FindPath(WorldPosition, _pathingTarget);
            Point moveVector = _pathFinder.GetMoveVectorToNextPathPoint(WorldPosition);
            Move(moveVector);
            if(moveVector != Point.Zero) SetSpriteName(GameConstants.PointToDirection(moveVector));

            base.Update(gametime);
        }
        #endregion
    }
}
