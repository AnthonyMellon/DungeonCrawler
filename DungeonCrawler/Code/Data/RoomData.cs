using System.Collections.Generic;

namespace DungeonCrawler.Code.Data
{
    public class RoomData
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public List<List<int>> Tiles { get; set; }
    }
}
