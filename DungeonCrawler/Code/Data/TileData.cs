namespace DungeonCrawler.Code.Data
{
    public class TileData
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public TileSpritePosition SpritePosition { get; set; }
        public bool IsCollidable { get; set; }
    }

    public class TileSpritePosition
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}