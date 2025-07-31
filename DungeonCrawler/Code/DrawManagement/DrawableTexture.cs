using DungeonCrawler.Code.Scenes;
using DungeonCrawler.Code.UI.Utils;
using DungeonCrawler.Code.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonCrawler.Code.DrawManagement
{
    internal class DrawableTexture : Drawable
    {
        public Texture2D Texture { get; set; }

        public DrawableTexture(
            Texture2D texture,
            Point position,
            Color color,
            GameConstants.GameLayers layer,
            Scene scene,
            bool visible = true) :
            base(layer, scene, visible)
        {
            Texture = texture;
            Position = position;
            Color = color;

            if (texture == null) Size = Point.Zero;
            else Size = texture.Bounds.Size;
        }

        public DrawableTexture(
            Texture2D texture,
            Color color,
            GameConstants.GameLayers layer,
            AnchorPoints anchorPoints,
            Size size,
            Offset offset,
            DynamicRectangle.FitTypes fitType,
            DynamicRectangle.GrowFroms growFrom,
            DynamicRectangle parent,
            Scene scene,
            bool visible = true) :
            base(layer, anchorPoints, size, offset, fitType, growFrom, parent, scene, visible)
        {
            Texture = texture;
            Color = color;
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            if (Texture == null) return;

            spritebatch.Draw(
                Texture,
                Rectangle.Rectangle,
                Texture.Bounds,
                Color,
                0,
                Vector2.Zero,
                SpriteEffects.None,
                GameConstants.GameLayerToLayer(Layer)
            );
        }

        public override void Draw(SpriteBatch spritebatch, Rectangle destinationRectangle)
        {
            if (Texture == null) return;

            spritebatch.Draw(
                Texture,
                destinationRectangle,
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
