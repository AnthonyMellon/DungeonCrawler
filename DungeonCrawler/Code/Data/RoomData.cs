using System.Xml.Serialization;

namespace DungeonCrawler.Code.Data
{
    public class RoomData
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int[] Tiles { get; set; }
    }
}
