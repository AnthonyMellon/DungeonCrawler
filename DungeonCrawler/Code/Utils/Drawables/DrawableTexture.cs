using DungeonCrawler.Code.DrawManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonCrawler.Code.Utils.Drawables
{
    internal class DrawableTexture : Drawable
    {
        public Texture2D Texture { get; set; }        

        public DrawableTexture(
            Texture2D texture,
            Point position,
            Color color, GameConstants.GameLayers layer,
            DrawManager.DrawTargets drawTarget = DrawManager.DrawTargets.None,
            bool visible = true) :
            base(layer, drawTarget, visible)
        {
            Texture = texture;
            Position = position;
            Color = color;
            Layer = layer;

            if (texture == null) Size = Point.Zero;
            else Size = texture.Bounds.Size;
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            if (Texture == null) return;

            spritebatch.Draw(
                Texture,
                Rectangle,
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
