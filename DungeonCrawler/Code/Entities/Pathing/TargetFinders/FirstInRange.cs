using DungeonCrawler.Code.Utils.Math;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace DungeonCrawler.Code.Entities.Pathing.TargetFinders
{
    internal class FirstInRange : ITargetFinder
    {
        #region publics

        public Entity FindTarget(Point currentPosition)
        {
            if (_potentialTargets == null || _potentialTargets.Count == 0)
            {
                return null;
            }

            for (int i = 0; i < _potentialTargets.Count; i++)
            {
                if (PointExtras.Distance(currentPosition, _potentialTargets[i].WorldPosition) <= _range)
                {
                    return _potentialTargets[i];
                }
            }
            return null;
        }

        public FirstInRange(float range, List<Entity> potentialTargets)
        {
            _range = range;
            _potentialTargets = potentialTargets;
        }
        #endregion

        #region privates
        private float _range;
        private List<Entity> _potentialTargets;
        #endregion
    }
}
