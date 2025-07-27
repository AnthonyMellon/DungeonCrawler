using DungeonCrawler.Code.Utils;
using DungeonCrawler.Code.Utils.Drawables;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace DungeonCrawler.Code.DrawManagement
{
    internal class DrawTarget
    {
        public List<Drawable> DrawList { get; private set; } = new List<Drawable>();
        public RenderTarget2D RenderTarget { get; private set; }

        public void Init(GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
        }

        public void Resize(int width, int height)
        {
            _width = width;
            _height = height;

            RenderTarget?.Dispose();
            RenderTarget = new RenderTarget2D(_graphicsDevice, _width, _height);
        }

        public void RegisterDrawable(Drawable drawable)
        {
            DrawList.Add(drawable);
        }

        public void DeregisterDrawable(Drawable drawable)
        {
            DrawList.Remove(drawable);
        }        

        private int _width;
        private int _height;
        private GraphicsDevice _graphicsDevice;
    }
}
