using DungeonCrawler.Code.Utils.Math;
using Microsoft.Xna.Framework;

namespace DungeonCrawler.Code.Entities.Pathing
{
    internal class PathFinder
    {
        #region publics

        public void FindPath(Point currentPosition, Entity target)
        {
            if (target == null) return;

            _currentPathPoint = target.WorldPosition;
        }

        public Point GetMoveVectorToNextPathPoint(Point currentPosition)
        {
            Point MoveVector = Point.Zero;

            if (_currentPathPoint.Y > currentPosition.Y) MoveVector += PointExtras.Up;
            else if (_currentPathPoint.Y < currentPosition.Y) MoveVector += PointExtras.Down;

            if (_currentPathPoint.X < currentPosition.X) MoveVector += PointExtras.Left;
            else if (_currentPathPoint.X > currentPosition.X) MoveVector += PointExtras.Right;

            return MoveVector;
        }

        #endregion

        #region privates
        private Point _currentPathPoint; // There eventually (once the map is implemented)
                                         // will be a list of path points to follow
        #endregion
    }
}
