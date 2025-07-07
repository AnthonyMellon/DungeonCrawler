using Microsoft.Xna.Framework.Graphics;
using System;

namespace DungeonCrawler.Code.Utils
{
    internal class GameTexture
    {
        private string Path = "";

        public Texture2D Texture
        {
            get
            {
                if (b_texture == null) b_texture = LoadTexture();
                return b_texture;
            }
        }
        private Texture2D b_texture;

        public GameTexture(string path)
        {
            Path = path;
        }

        /// <summary>
        /// Load the texture from <see cref="Path"/>
        /// </summary>
        /// <returns>Loaded Texture</returns>
        /// <exception cref="Exception"></exception>
        private Texture2D LoadTexture()
        {
            if (GameValues.GameContent == null)
            {
                throw new Exception("Faild to load texture, no contentManager provided");
            }

            return GameValues.GameContent.Load<Texture2D>(Path);
        }
    }
}
