using DungeonCrawler.Code.UI;
using DungeonCrawler.Code.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace DungeonCrawler.Code.Entities
{
    internal class Entity : Dynamic
    {
        // Sprite Stuff
        private static Point TEXTURE_SIZE = new Point(256, 256);
        protected GameTexture SpriteSheet;
        protected Dictionary<Textures, Rectangle> TextureRectangles = new Dictionary<Textures, Rectangle>()
        {
                { Textures.Front, new Rectangle(new Point(TEXTURE_SIZE.X * 0, TEXTURE_SIZE.Y * 0), TEXTURE_SIZE) },
                { Textures.Back, new Rectangle(new Point(TEXTURE_SIZE.X * 1, TEXTURE_SIZE.Y * 0), TEXTURE_SIZE) },
                { Textures.Left, new Rectangle(new Point(TEXTURE_SIZE.X * 0, TEXTURE_SIZE.Y * 1), TEXTURE_SIZE) },
                { Textures.Right, new Rectangle(new Point(TEXTURE_SIZE.X * 1, TEXTURE_SIZE.Y * 1), TEXTURE_SIZE) }
        };
        protected Textures CurrentTexture = Textures.Front;
        private const float SCALE = 0.25f;

        // Camera Stuff
        protected Camera Camera;
        private Vector2 ScreenPosition => (WorldPosition - Camera.WorldPosition) + new Vector2(Camera.CenterPosition.X, Camera.CenterPosition.Y);

        // Movement
        public Vector2 WorldPosition = new Vector2(0, 0);
        protected float MoveSpeed = 5;

        // The manager this entity is added to
        protected EntityManager EntityManager;

        protected enum Textures
        {
            Front,
            Back,
            Left,
            Right
        }

        public Entity(Camera camera, EntityManager entityManager)
        {
            Camera = camera;
            EntityManager = entityManager;
        }


        protected void SetupTextures(string spriteSheetPath)
        {
            SpriteSheet = new GameTexture(spriteSheetPath);
        }

        protected void Move(Vector2 moveVector)
        {
            WorldPosition += moveVector;
        }

        protected void MoveUp()
        {
            Move(new Vector2(0, -1 * MoveSpeed));
            CurrentTexture = Textures.Back;
        }
        protected void MoveDown()
        {
            Move(new Vector2(0, MoveSpeed));
            CurrentTexture = Textures.Front;
        }
        protected void MoveLeft()
        {
            Move(new Vector2(-1 * MoveSpeed, 0));
            CurrentTexture = Textures.Left;
        }
        protected void MoveRight()
        {
            Move(new Vector2(MoveSpeed, 0));
            CurrentTexture = Textures.Right;
        }

        protected override void Draw(GameTime gametime, SpriteBatch graphics)
        {
            graphics.Draw(
                texture: SpriteSheet.Texture,
                position: ScreenPosition,
                sourceRectangle: TextureRectangles[CurrentTexture],
                color: Color.White,
                rotation: 0,
                origin: new Vector2(0, 0),
                scale: SCALE,
                effects: SpriteEffects.None,
                layerDepth: 0
                );
        }

        protected override void Update(GameTime gametime) { }
    }
}
