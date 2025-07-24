using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonCrawler.Code.Utils.Drawables
{
    internal class DrawableTexture : Drawable
    {
        public Texture2D Texture { get; set; }
        public Rectangle DestinationRectangle { get; set; }
        public GameConstants.GameLayers Layer { get; set; }

        public DrawableTexture(
            Texture2D texture,
            Rectangle destinationRectangle,
            Color color, GameConstants.GameLayers layer,
            DrawManager.DrawTargets drawTarget,
            bool visible = true) :
            base(drawTarget, visible)
        {
            Texture = texture;
            DestinationRectangle = destinationRectangle;
            Color = color;
            Layer = layer;
        }
        public DrawableTexture(
            Texture2D texture,
            GameConstants.GameLayers layer,
            DrawManager.DrawTargets drawTarget,
            bool visible = true) :
            base(drawTarget, visible)
        {
            Texture = texture;
            Layer = layer;
        }

        public override void Draw(SpriteBatch spritebatch, GameTime gameTime)
        {
            if (Texture == null) return;

            spritebatch.Draw(
                Texture,
                DestinationRectangle,
                Texture.Bounds,
                Color,
                0,
                Vector2.Zero,
                SpriteEffects.None,
                GameConstants.GameLayerToLayer(Layer)
            );
        }
    }
}
