using Microsoft.VisualBasic.Logging;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler.Code.Utils.Drawables
{
    internal interface IDrawableText
    {
        public string Text { get; protected set; }
        public SpriteFont Font { get; protected set; }
        public Vector2 Position { get; protected set; }
        public Color Color { get; protected set; }
        public float Rotation { get; protected set; }
        public Vector2 Origin { get; protected set; }
        public float Scale { get; protected set; }
        public GameConstants.GameLayers Layer { get; protected set; }
    }
}
