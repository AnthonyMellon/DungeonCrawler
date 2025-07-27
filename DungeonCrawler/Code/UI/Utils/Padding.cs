using DungeonCrawler.Code.Utils;
using System.Threading;

namespace DungeonCrawler.Code.UI.Utils
{
    internal class Padding
    {
        public static Padding Positive => new Padding(1);
        public static Padding Zero => new Padding(0);
        public static Padding Negative => new Padding(-1);

        public int X => _padding.X;
        public int Y => _padding.Y;
        public int Z => _padding.Z;
        public int W => _padding.W;

        public Padding(int padding)
        {
            _padding = new Point4(padding, padding, padding, padding);
        }

        public Padding(int horizontal, int vertical)
        {
            _padding = new Point4(horizontal, horizontal, vertical, vertical);
        }

        public Padding(int x, int y, int z, int w)
        {
            _padding = new Point4(x, y, z, w);
        }

        public Padding(Point4 padding)
        {
            _padding = padding;
        }

        private Point4 _padding;

        public static Padding operator * (int multiplier, Padding padding)
        {
            return new Padding(
                padding.X * multiplier,
                padding.Y * multiplier,
                padding.Z * multiplier,
                padding.W * multiplier);
        }

        public static Padding operator * (Padding padding, int multiplier)
        {
            return new Padding(
                padding.X * multiplier,
                padding.Y * multiplier,
                padding.Z * multiplier,
                padding.W * multiplier);
        }
    }
}
