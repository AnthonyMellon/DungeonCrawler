using Microsoft.Xna.Framework;

namespace DungeonCrawler.Code.UI.Utils
{
    internal class Offset
    {
        public static Offset Positive => new Offset(1);
        public static Offset Zero => new Offset(0);
        public static Offset Negative => new Offset(-1);

        public int X => _offset.X;
        public int Y => _offset.Y;

        public Offset(int x, int y)
        {
            _offset = new Point(x, y);
        }

        public Offset(int offset)
        {
            _offset = new Point(offset, offset);
        }

        public static Offset operator *(int multiplier, Offset offset)
        {
            return new Offset(
                offset.X * multiplier,
                offset.Y * multiplier);
        }

        public static Offset operator *(Offset offset, int multiplier)
        {
            return new Offset(
                offset.X * multiplier,
                offset.Y * multiplier);
        }

        private Point _offset;
    }
}
