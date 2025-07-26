using Microsoft.Xna.Framework;

namespace DungeonCrawler.Code.Utils.MathExtras
{
    internal static class PointExtras
    {
        public static Point Up => new Point(0, -1);
        public static Point Down => new Point(0, 1);
        public static Point Left => new Point(-1, 0);
        public static Point Right => new Point(1, 0);

        public static float Distance(Point point1, Point point2)
        {
            return Vector2.Distance(
                new Vector2(point1.X, point1.Y),
                new Vector2(point2.X, point2.Y));
        }
    }
}
