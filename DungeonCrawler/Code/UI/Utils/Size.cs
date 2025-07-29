using Microsoft.Xna.Framework;

namespace DungeonCrawler.Code.UI.Utils
{
    internal class Size
    {
        public static Size Positive => new Size(1);
        public static Size Zero => new Size(0);
        public static Size Negative => new Size(-1);

        public int Width => _size.X;
        public int Height => _size.Y;

        public Size(int size)
        {
            _size = new Point(size, size);
        }

        public Size(int width, int height)
        {
            _size = new Point(width, height);
        }

        public Size(Point size)
        {
            _size = size;
        }

        public static Size operator *(int multiplier, Size size)
        {
            return new Size(
                size.Width * multiplier,
                size.Height * multiplier);
        }

        public static Size operator *(Size size, int multiplier)
        {
            return new Size(
                size.Width * multiplier,
                size.Height * multiplier);
        }

        private Point _size;
    }
}
