using DungeonCrawler.Code.Utils;
using DungeonCrawler.Code.Utils.Drawables;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace DungeonCrawler.Code
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
            BuildRenderTargets(GameValues.ScreenSize.X, GameValues.ScreenSize.Y);
            GameEvents.OnScreenSizeChange += BuildRenderTargets;
        }

        public static void OnScreenSizeChanged(int width, int height)
        {
            BuildRenderTargets(width, height);
        }

        public static void Draw(SpriteBatch spritebatch, GraphicsDevice graphicsDevice, GameTime gametime)
        {
            GenerateAllLayerTextures(spritebatch, graphicsDevice, gametime);
            
            spritebatch.Begin(blendState: BlendState.NonPremultiplied);
            for (int i = 0; i < _drawOrder.Count; i++)
            {
                DrawTargets target = _drawOrder[i];

                RenderTarget2D renderTarget = _drawTargetToRenderTarget[target];
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
            List<Drawable> drawList = _drawTargetToDrawList[drawTarget];
            drawList.Add(drawable);
        }

        public static void DeregisterDrawable(DrawTargets drawTarget, Drawable drawable)
        {
            List<Drawable> drawList = _drawTargetToDrawList[drawTarget];
            drawList.Remove(drawable);
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

        private static List<Drawable> _worldLayer = new List<Drawable>();
        private static List<Drawable> _uiLayer = new List<Drawable>();
        private static List<Drawable> _developmentLayer = new List<Drawable>();
        private static List<Drawable> _noneLayer = new List<Drawable>();

        private static RenderTarget2D _worldRenderTarget;
        private static RenderTarget2D _uiRenderTarget;
        private static RenderTarget2D _developmentRenderTarget;
        private static RenderTarget2D _noneRenderTarget;

        private static List<DrawTargets> _drawOrder = new List<DrawTargets>()
        {
            DrawTargets.World,
            DrawTargets.UI,
            DrawTargets.Development,
        };

        private static Dictionary<DrawTargets, List<Drawable>> _drawTargetToDrawList = new Dictionary<DrawTargets, List<Drawable>>()
        {
            { DrawTargets.World, _worldLayer },
            { DrawTargets.UI, _uiLayer },
            { DrawTargets.Development,  _developmentLayer},
            { DrawTargets.None, _noneLayer }
        };

        private static Dictionary<DrawTargets, RenderTarget2D> _drawTargetToRenderTarget = new Dictionary<DrawTargets, RenderTarget2D>()
        {
            { DrawTargets.World, _worldRenderTarget },
            { DrawTargets.UI, _uiRenderTarget },
            { DrawTargets.Development, _developmentRenderTarget },
            { DrawTargets.None, _noneRenderTarget }
        };

        private static void GenerateLayerTexture(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, GameTime gameTime, DrawTargets drawTarget)
        {
            RenderTarget2D renderTarget = _drawTargetToRenderTarget[drawTarget];
            List<Drawable> drawList = _drawTargetToDrawList[drawTarget];

            graphicsDevice.SetRenderTarget(renderTarget);
            graphicsDevice.Clear(Color.Transparent);

            spriteBatch.Begin(blendState: BlendState.NonPremultiplied, sortMode: SpriteSortMode.BackToFront);
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

        private static void BuildRenderTargets(int width, int height)
        {
            for (int i = 0; i < _drawOrder.Count; i++)
            {
                BuildRenderTarget(_drawOrder[i], width, height);
            }
        }

        private static void BuildRenderTarget(DrawTargets drawTarget, int width, int height)
        {
            RenderTarget2D renderTarget = _drawTargetToRenderTarget[drawTarget];

            renderTarget?.Dispose();
            renderTarget = new RenderTarget2D(_graphicsDevice, width, height);

            _drawTargetToRenderTarget[drawTarget] = renderTarget;
        }
    }
}
