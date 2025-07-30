using DungeonCrawler.Code.Utils;
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
            InitDrawTargets();
            ResizeDrawTargets(GameValues.ScreenSize.X, GameValues.ScreenSize.Y);
            GameEvents.OnScreenSizeChange += ResizeDrawTargets;
        }

        public static void Draw(SpriteBatch spritebatch, GraphicsDevice graphicsDevice, GameTime gametime)
        {
            GenerateAllLayerTextures(spritebatch, graphicsDevice, gametime);

            spritebatch.Begin(blendState: BlendState.NonPremultiplied);
            for (int i = 0; i < _drawOrder.Count; i++)
            {
                DrawTarget target = _drawOrder[i];

                RenderTarget2D renderTarget = target.RenderTarget;
                if (renderTarget == null) continue;
                spritebatch.Draw(
                    renderTarget,
                    Vector2.Zero,
                    Color.White);
            }
            spritebatch.End();
        }

        public static void PreDraw()
        {
            for (int i = 0; i < _complexDrawables.Count; i++)
            {
                _complexDrawables[i].PreDraw();
            }
        }

        public static void RegisterDrawable(DrawTargets drawTarget, Drawable drawable)
        {
            DrawTarget target = _getDrawTarget[drawTarget];
            target.RegisterDrawable(drawable);
        }

        public static void DeregisterDrawable(DrawTargets drawTarget, Drawable drawable)
        {
            DrawTarget target = _getDrawTarget[drawTarget];
            target.DeregisterDrawable(drawable);
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

        private static GraphicsDevice _graphicsDevice;

        private static List<ComplexDrawable> _complexDrawables = new List<ComplexDrawable>();

        private static DrawTarget _worldRenderTarget = new DrawTarget();
        private static DrawTarget _uiRenderTarget = new DrawTarget();
        private static DrawTarget _developmentRenderTarget = new DrawTarget();
        private static DrawTarget _noneRenderTarget = new DrawTarget();

        private static List<DrawTarget> _drawOrder = new List<DrawTarget>()
        {
            _worldRenderTarget,
            _uiRenderTarget,
            _developmentRenderTarget,
        };

        private static Dictionary<DrawTargets, DrawTarget> _getDrawTarget = new Dictionary<DrawTargets, DrawTarget>()
        {
            { DrawTargets.World, _worldRenderTarget },
            { DrawTargets.UI, _uiRenderTarget },
            { DrawTargets.Development, _developmentRenderTarget },
            { DrawTargets.None, _noneRenderTarget }
        };

        private static void GenerateLayerTexture(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, GameTime gameTime, DrawTarget drawTarget)
        {
            RenderTarget2D renderTarget = drawTarget.RenderTarget;
            List<Drawable> drawList = drawTarget.DrawList;

            graphicsDevice.SetRenderTarget(renderTarget);
            graphicsDevice.Clear(Color.Transparent);

            spriteBatch.Begin(blendState: BlendState.AlphaBlend, sortMode: SpriteSortMode.BackToFront);
            for (int i = 0; i < drawList.Count; i++)
            {
                drawList[i].Draw(spriteBatch);
            }
            spriteBatch.End();

            graphicsDevice.SetRenderTarget(null);
        }

        private static void GenerateAllLayerTextures(SpriteBatch spritebatch, GraphicsDevice graphicsDevice, GameTime gametime)
        {
            for (int i = 0; i < _drawOrder.Count; i++)
            {
                GenerateLayerTexture(spritebatch, graphicsDevice, gametime, _drawOrder[i]);
            }
        }

        private static void InitDrawTargets()
        {
            for (int i = 0; i < _drawOrder.Count; i++)
            {
                _drawOrder[i].Init(_graphicsDevice);
            }
        }
        private static void ResizeDrawTargets(int width, int height)
        {
            for (int i = 0; i < _drawOrder.Count; i++)
            {
                _drawOrder[i].Resize(width, height);
            }
        }
    }
}
