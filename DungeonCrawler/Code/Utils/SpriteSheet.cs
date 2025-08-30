using DungeonCrawler.Code.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace DungeonCrawler.Code.Utils
{
    internal class SpriteSheet
    {
        #region publics
        public string Path
        {
            get
            {
                return _path;
            }
            set
            {
                _path = value;
                Sheet = null;
            }
        }

        public Texture2D Sheet
        {
            get
            {
                if (_sheet == null) _sheet = LoadSpriteSheet();
                return _sheet;
            }
            private set
            {
                _sheet = value;
            }
        }

        public Dictionary<string, Rectangle> Sprites { get; set; }

        public Rectangle GetSprite(string sprite)
        {
            return Sprites[sprite];
        }


        public SpriteSheet(SpriteSheetData data)
        {
            Path = data.Path;
            Sprites = data.SpriteRectangles;
        }
        #endregion

        #region privates
        private string _path;
        private Texture2D _sheet;

        private Texture2D LoadSpriteSheet()
        {
            if (GameValues.GameContent == null)
            {
                throw new Exception("Faild to load texture, no contentManager provided");
            }

            return GameValues.GameContent.Load<Texture2D>(Path);
        }
        #endregion
    }
}
