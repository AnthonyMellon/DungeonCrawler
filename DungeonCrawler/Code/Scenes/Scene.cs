using DungeonCrawler.Code.DrawManagement;
using DungeonCrawler.Code.UI.Utils;
using DungeonCrawler.Code.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace DungeonCrawler.Code.Scenes
{
    internal abstract class Scene : Dynamic
    {
        public bool Initialised { get; private set; }
        public RenderTarget2D RenderTarget { get; private set; }
        public List<Drawable> DrawList { get; private set; } = new List<Drawable>();
        public DynamicRectangle ScreenRectangle { get; private set; } = DynamicRectangle.FillScreen;

        /// <summary>
        /// To be called when the scene is loaded
        /// </summary>
        /// <param name="content"></param>
        /// <param name="game"></param>
        public void DoInit(ContentManager content, Game game)
        {
            Initialised = false;
            ScreenRectangle.OnRectangleUpdated += UpdateRenderTarget;
            UpdateRenderTarget();
            Init(content, game);
            Initialised = true;
        }

        /// <summary>
        /// Called when the scene is loaded
        /// </summary>
        /// <param name="content"></param>
        /// <param name="game"></param>
        public abstract void Init(ContentManager content, Game game);

        public void UpdateRenderTarget()
        {
            RenderTarget?.Dispose();
            RenderTarget = new RenderTarget2D(GameValues.GraphicsDevice, ScreenRectangle.ScreenSize.X, ScreenRectangle.ScreenSize.Y);
        }

        public void RegisterDrawable(Drawable drawable)
        {
            DrawList.Add(drawable);
        }

        public void DeregisterDrawable(Drawable drawable)
        {
            DrawList.Remove(drawable);
        }

        public void Draw()
        {

        }

        public Scene(bool enabled = false) :
            base(enabled)
        {
        }
    }
}
