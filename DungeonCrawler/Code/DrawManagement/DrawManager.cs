using DungeonCrawler.Code.Scenes;
using DungeonCrawler.Code.Utils.Drawables;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace DungeonCrawler.Code.DrawManagement
{
    internal static class DrawManager
    {
        public enum DrawTargets
        {
            World,
            UI,
            Development,
            None
        }

        public static void Setup(GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
        }

        public static void RegisterComplexDrawable(ComplexDrawable complexDrawable)
        {
            if (_complexDrawables.Contains(complexDrawable)) return;

            _complexDrawables.Add(complexDrawable);
        }

        public static void DeregisterComplexDrawable(ComplexDrawable complexDrawable)
        {
            _complexDrawables.Remove(complexDrawable);
        }

        public static void PreDraw()
        {
            for (int i = 0; i < _complexDrawables.Count; i++)
            {
                _complexDrawables[i].PreDraw();
            }
        }

        public static void Draw(SpriteBatch spritebatch, GraphicsDevice graphicsDevice, GameTime gametime)
        {
            GenerateAllActiveSceneTextures(spritebatch, graphicsDevice, gametime);

            spritebatch.Begin(blendState: BlendState.NonPremultiplied);
            for (int i = 0; i < SceneManager.ActiveScenes.Count; i++)
            {
                Scene scene = SceneManager.ActiveScenes[i];
                spritebatch.Draw(
                    scene.RenderTarget,
                    scene.ScreenRectangle.Rectangle,
                    Color.White);
            }
            spritebatch.End();
        }

        private static GraphicsDevice _graphicsDevice;
        private static List<ComplexDrawable> _complexDrawables = new List<ComplexDrawable>();

        private static void GenerateAllActiveSceneTextures(SpriteBatch spritebatch, GraphicsDevice graphicsDevice, GameTime gametime)
        {
            for (int i = 0; i < SceneManager.ActiveScenes.Count; i++)
            {
                GenerateSceneTexture(spritebatch, graphicsDevice, gametime, SceneManager.ActiveScenes[i]);
            }
        }

        private static void GenerateSceneTexture(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, GameTime gameTime, Scene scene)
        {
            RenderTarget2D renderTarget = scene.RenderTarget;
            List<Drawable> drawList = scene.DrawList;

            graphicsDevice.SetRenderTarget(renderTarget);
            graphicsDevice.Clear(Color.Transparent);

            spriteBatch.Begin(blendState: BlendState.AlphaBlend, sortMode: SpriteSortMode.BackToFront);
            for (int i = 0; i < drawList.Count; i++)
            {
                Drawable drawable = drawList[i];
                if (!drawable.Visible) continue;
                drawable.Draw(spriteBatch);
            }
            spriteBatch.End();

            graphicsDevice.SetRenderTarget(null);
        }
    }
}
