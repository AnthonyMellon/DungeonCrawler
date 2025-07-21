using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler.Code.Utils.Math
{
    internal static class PointExtras
    {
        public static Point Up => new Point(0, 1);
        public static Point Down => new Point(0, -1);
        public static Point Left => new Point(-1, 0);
        public static Point Right => new Point(0, 1);

        public static float Distance(Point point1, Point point2)
        {
            return Vector2.Distance(
                new Vector2(point1.X, point1.Y),
                new Vector2(point2.X, point2.Y));
        }
    }
}
