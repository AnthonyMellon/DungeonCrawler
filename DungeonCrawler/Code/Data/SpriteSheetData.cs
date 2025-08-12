using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler.Code.Data
{
    public class SpriteSheetData
    {
        public string Path { get; set; }
        public Dictionary<string, Rectangle> SpriteRectangles { get; set; }
    }    
}
