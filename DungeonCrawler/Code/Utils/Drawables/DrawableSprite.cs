using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonCrawler.Code.Utils.Drawables
{
    internal class DrawableSprite : Drawable
    {
        #region publics
        public SpriteSheet SpriteSheet { get; private set; }
        public Rectangle DestinationRectangle { get; private set; }
        public Rectangle SourceRectangle { get; private set; }
        public GameConstants.GameLayers Layer { get; set; }
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
            }
        }

        public DrawableSprite(
            SpriteSheet spriteSheet,
            Rectangle destinationRectangle,
            string currentSpriteName,
            Color color,
            GameConstants.GameLayers layer,
            DrawManager.DrawTargets drawTarget,
            bool visible = true) :
            base(drawTarget, visible)
        {
            SpriteSheet = spriteSheet;
            CurrentSpriteName = currentSpriteName;
            SetDestinationRectangle(destinationRectangle);
            Color = color;
            Layer = layer;
        }

        public DrawableSprite(
            SpriteSheet spriteSheet,
            Rectangle destinationRectangle,
            string currentSpriteName,
            GameConstants.GameLayers layer,
            DrawManager.DrawTargets drawTarget,
            bool visible = true) :
            base(drawTarget, visible)
        {
            SpriteSheet = spriteSheet;
            CurrentSpriteName = currentSpriteName;
            SetDestinationRectangle(destinationRectangle);
            Color = Color.White;
            Layer = layer;
        }

        public DrawableSprite(
            SpriteSheet spriteSheet,
            Point position,
            string currentSpriteName,
            GameConstants.GameLayers layer,
            DrawManager.DrawTargets drawTarget,
            bool visible = true) :
            base(drawTarget, visible)
        {
            SpriteSheet = spriteSheet;
            CurrentSpriteName = currentSpriteName;
            SetDestinationRectangle(position);
            Color = Color.White;
            Layer = layer;
        }

        public void SetDestinationRectangle(Rectangle destinationRectangle)
        {
            DestinationRectangle = destinationRectangle;
        }

        public void SetDestinationRectangle(Point position)
        {
            DestinationRectangle = new Rectangle(
                position.X,
                position.Y,
                SpriteSheet.Sprites[CurrentSpriteName].Width,
                SpriteSheet.Sprites[CurrentSpriteName].Height
                );
        }

        public override void Draw(SpriteBatch spritebatch, GameTime gameTime)
        {
            spritebatch.Draw(
                SpriteSheet.Sheet,
                DestinationRectangle,
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
