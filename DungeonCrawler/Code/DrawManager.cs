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
            None
        }

        public static void Setup(GraphicsDevice graphicsDevice)
        {
            _worldRenderTarget?.Dispose();
            _worldRenderTarget = new RenderTarget2D(graphicsDevice, 1920, 1080);
            _drawableTypeToRenderTarget[DrawTargets.World] = _worldRenderTarget;

            _uiRenderTarget?.Dispose();
            _uiRenderTarget = new RenderTarget2D(graphicsDevice, 1920, 1080);
            _drawableTypeToRenderTarget[DrawTargets.UI] = _uiRenderTarget;
        }

        public static void Draw(SpriteBatch spritebatch, GraphicsDevice graphicsDevice, GameTime gametime)
        {
            GenerateAllLayerTextures(spritebatch, graphicsDevice, gametime);

            graphicsDevice.Clear(GameConstants.DEFAULT_COLOR);
            spritebatch.Begin(blendState: BlendState.NonPremultiplied);
            for (int i = 0; i < _drawOrder.Count; i++)
            {
                DrawTargets target = _drawOrder[i];                

                RenderTarget2D renderTarget = _drawableTypeToRenderTarget[target];
                if (renderTarget == null) continue;
                spritebatch.Draw(
                    renderTarget,
                    Vector2.Zero,
                    Color.White);
            }
            spritebatch.End();
        }

        public static void RegisterDrawable(DrawTargets drawTarget, Drawable drawable)
        {
            List<Drawable> drawList = _drawableTypeToDrawList[drawTarget];
            drawList.Add(drawable);
        }

        public static void DeregisterDrawable(DrawTargets drawTarget, Drawable drawable)
        {
            List<Drawable> drawList = _drawableTypeToDrawList[drawTarget];
            drawList.Remove(drawable);
        }

        private static List<Drawable> _worldLayer = new List<Drawable>();
        private static List<Drawable> _uiLayer = new List<Drawable>();
        private static List<Drawable> _noneLayer = new List<Drawable>();

        private static RenderTarget2D _worldRenderTarget;
        private static RenderTarget2D _uiRenderTarget;
        private static RenderTarget2D _noneRenderTarget;

        private static List<DrawTargets> _drawOrder = new List<DrawTargets>()
        {
            DrawTargets.World,
            DrawTargets.UI
        };

        private static Dictionary<DrawTargets, List<Drawable>> _drawableTypeToDrawList = new Dictionary<DrawTargets, List<Drawable>>()
        {
            { DrawTargets.World, _worldLayer },
            { DrawTargets.UI, _uiLayer },
            { DrawTargets.None, _noneLayer }
        };

        private static Dictionary<DrawTargets, RenderTarget2D> _drawableTypeToRenderTarget = new Dictionary<DrawTargets, RenderTarget2D>()
        {
            { DrawTargets.World, _worldRenderTarget },
            { DrawTargets.UI, _uiRenderTarget },
            { DrawTargets.None, _noneRenderTarget }
        };

        private static void GenerateLayerTexture(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, GameTime gameTime, DrawTargets drawTarget)
        {
            RenderTarget2D renderTarget = _drawableTypeToRenderTarget[drawTarget];
            List<Drawable> drawList = _drawableTypeToDrawList[drawTarget];
                
            graphicsDevice.SetRenderTarget(renderTarget);
            graphicsDevice.Clear(GameConstants.DEFAULT_COLOR);

            spriteBatch.Begin();
            for (int i = 0; i < drawList.Count; i++)
            {
                drawList[i].Draw(spriteBatch);
            }
            spriteBatch.End();

            graphicsDevice.SetRenderTarget(null);
            graphicsDevice.Clear(GameConstants.DEFAULT_COLOR);
        }

        private static void GenerateAllLayerTextures(SpriteBatch spritebatch, GraphicsDevice graphicsDevice, GameTime gametime)
        {
            for (int i = 0; i < _drawOrder.Count; i++)
            {
                GenerateLayerTexture(spritebatch, graphicsDevice, gametime, _drawOrder[i]);
            }
        }
    }
}
