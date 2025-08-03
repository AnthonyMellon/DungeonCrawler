namespace DungeonCrawler.Code.Utils.TileMaps
{
    public class TileDataCollection
    {
        public TileDataCollection() { }

        public TileData[] Tiles { get; set; }
    }

    public class TileData
    {
        public TileData() { }

        public int ID { get; set; }
        public string Name { get; set; }
        public TileSpritePosition SpritePosition { get; set; }
        public bool IsCollidable { get; set; }
    }

    public class TileSpritePosition
    {
        public TileSpritePosition() { }

        public int X { get; set; }
        public int Y { get; set; }
    }
}