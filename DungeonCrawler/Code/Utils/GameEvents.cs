namespace DungeonCrawler.Code.Utils
{
    internal static class GameEvents
    {
        public delegate void ScreenSizeChaangeHandler(int width, int height);
        /// <summary>
        /// To be called when the game windows size changes
        /// </summary>
        public static ScreenSizeChaangeHandler OnScreenSizeChange;
    }
}
