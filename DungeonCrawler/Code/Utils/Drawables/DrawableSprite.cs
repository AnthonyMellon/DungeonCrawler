using DungeonCrawler.Code.DrawManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonCrawler.Code.Utils.Drawables
{
    internal class DrawableSprite : Drawable
    {
        #region publics
        public SpriteSheet SpriteSheet { get; private set; }
        public Rectangle SourceRectangle { get; private set; }        
        public string CurrentSpriteName
        {
            get
            {
                return _currentSpriteName;
            }
            set
            {
                _currentSpriteName = value;
                SourceRectangle = SpriteSheet.GetSprite(value);
                Size = SpriteSheet.Sprites[CurrentSpriteName].Size;
            }
        }

        public DrawableSprite(
            SpriteSheet spriteSheet,
            Point position,
            string currentSpriteName,
            GameConstants.GameLayers layer,
            DrawManager.DrawTargets drawTarget = DrawManager.DrawTargets.None,
            bool visible = true) :
            base(layer, drawTarget, visible)
        {
            SpriteSheet = spriteSheet;
            CurrentSpriteName = currentSpriteName;
            Position = position;
            Color = Color.White;
            Layer = layer;
        }

        public DrawableSprite(
            SpriteSheet spriteSheet,
            Point position,
            string currentSpriteName,
            Color color,
            GameConstants.GameLayers layer,
            DrawManager.DrawTargets drawTarget = DrawManager.DrawTargets.None,
            bool visible = true) :
            base(layer, drawTarget, visible)
        {
            SpriteSheet = spriteSheet;
            CurrentSpriteName = currentSpriteName;
            Position = position;
            Color = color;
            Layer = layer;
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            Draw(spritebatch, Rectangle.Rectangle);
        }

        public override void Draw(SpriteBatch spritebatch, Rectangle destinationRectangle)
        {
            spritebatch.Draw(
                SpriteSheet.Sheet,
                destinationRectangle,
                SourceRectangle,
                Color,
                0,              // Rotation
                Vector2.Zero,   // Origin       currently no use for these values  
                        SpriteEffects.None,
                GameConstants.GameLayerToLayer(Layer)
            );
        }
        #endregion

        #region privates
        private string _currentSpriteName;
        #endregion
    }
}
