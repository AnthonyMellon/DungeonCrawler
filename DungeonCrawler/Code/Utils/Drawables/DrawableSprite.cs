using Microsoft.Xna.Framework;


namespace DungeonCrawler.Code.Utils.Drawables
{
    internal class DrawableSprite
    {
        #region publics
        public SpriteSheet SpriteSheet { get; private set; }
        public Rectangle DestinationRectangle { get; private set; }
        public Rectangle SourceRectangle { get; private set; }
        public Color Color { get; set; }
        public GameConstants.GameLayers Layer { get; private set; }
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
        public DrawableSprite(SpriteSheet spriteSheet, Rectangle destinationRectangle, string currentSpriteName, Color color, GameConstants.GameLayers layer)
        {
            SpriteSheet = spriteSheet;
            SetDestinationRectangle(destinationRectangle);
            CurrentSpriteName = currentSpriteName;
            Color = color;
            Layer = layer;
        }

        public DrawableSprite(SpriteSheet spriteSheet, Rectangle destinationRectangle, string currentSpriteName, GameConstants.GameLayers layer)
        {
            SpriteSheet = spriteSheet;
            SetDestinationRectangle(destinationRectangle);
            CurrentSpriteName = currentSpriteName;
            Color = Color.White;
            Layer = layer;
        }

        public DrawableSprite(SpriteSheet spriteSheet, Point position, string currentSpriteName, GameConstants.GameLayers layer)
        {
            SpriteSheet = spriteSheet;
            SetDestinationRectangle(position);
            CurrentSpriteName = currentSpriteName;
            Color = Color.White;
            Layer = layer;
        }

        public DrawableSprite(SpriteSheet spriteSheet, Rectangle destinationRectangle, Color color, GameConstants.GameLayers layer)
        {
            SpriteSheet = spriteSheet;
            SetDestinationRectangle(destinationRectangle);
            SourceRectangle = spriteSheet.Sheet.Bounds;
            Color = color;
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
        #endregion

        #region privates
        private string _currentSpriteName;
        #endregion
    }
}
