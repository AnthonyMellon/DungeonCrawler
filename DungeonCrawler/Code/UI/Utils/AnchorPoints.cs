using Microsoft.Xna.Framework;

namespace DungeonCrawler.Code.UI.Utils
{
    internal class AnchorPoints
    {
        #region Statics
        public static AnchorPoints TopStretch => new AnchorPoints(0, 1, 0, 0);
        public static AnchorPoints BottomStretch => new AnchorPoints(0, 1, 1, 1);
        public static AnchorPoints LeftStretch => new AnchorPoints(0, 0, 0, 1);
        public static AnchorPoints RightStretch => new AnchorPoints(1, 1, 0, 1);
        public static AnchorPoints Center => new AnchorPoints(0.5f, 0.5f, 0.5f, 0.5f);
        public static AnchorPoints CenterTop => new AnchorPoints(0.5f, 0.5f, 0, 0);
        public static AnchorPoints CenterBottom => new AnchorPoints(0.5f, 0.5f, 1, 1);
        public static AnchorPoints CenterLeft => new AnchorPoints(0, 0, 0.5f, 0.5f);
        public static AnchorPoints CenterRight => new AnchorPoints(1, 1, 0.5f, 0.5f);
        public static AnchorPoints TopLeft => new AnchorPoints(0, 0, 0, 0);
        public static AnchorPoints TopRight => new AnchorPoints(1, 1, 0, 0);
        public static AnchorPoints BottomLeft => new AnchorPoints(0, 0, 1, 1);
        public static AnchorPoints BottomRight => new AnchorPoints(1, 1, 1, 1);
        public static AnchorPoints Fill => new AnchorPoints(0, 1, 0, 1);
        #endregion

        public float X => _anchorPoints.X;
        public float Y => _anchorPoints.Y;
        public float Z => _anchorPoints.Z;
        public float W => _anchorPoints.W;

        public AnchorPoints(Vector4 anchorPoints)
        {
            _anchorPoints = anchorPoints;
        }

        public AnchorPoints(float x, float y, float z, float w)
        {
            _anchorPoints = new Vector4(x, y, z, w);
        }

        private Vector4 _anchorPoints;
    }
}
