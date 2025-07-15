using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace DungeonCrawler.Code.Utils
{
    internal static class GameConstants
    {

        #region GameLayers
        public enum GameLayers
        {
            UI,
            World,
        }
        private static int _NumberOfGameLayers = Enum.GetValues<GameLayers>().Length;
        public static float GameLayerToLayer(GameLayers layer) => (float)layer / _NumberOfGameLayers;
        #endregion
    }
}
