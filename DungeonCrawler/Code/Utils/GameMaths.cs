namespace DungeonCrawler.Code.Utils
{
    /// <summary>
    /// 4D Vector of ints
    /// </summary>
    /// <param name="X"></param>
    /// <param name="Y"></param>
    /// <param name="Z"></param>
    /// <param name="W"></param>
    public struct Point4(int X, int Y, int Z, int W)
    {
        public int X = X;
        public int Y = Y;
        public int Z = Z;
        public int W = W;
    }

    internal static class GameMaths
    {
    }
}
