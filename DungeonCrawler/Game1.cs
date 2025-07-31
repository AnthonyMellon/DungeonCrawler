using DungeonCrawler.Code.DrawManagement;
using DungeonCrawler.Code.Input;
using DungeonCrawler.Code.Scenes;
using DungeonCrawler.Code.UI;
using DungeonCrawler.Code.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace DungeonCrawler
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Camera _mainCamera;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            Window.Title = GameConstants.WindowTitle;
#if DEVELOPMENT
            Window.Title = $"{GameConstants.WindowTitle} - DEVELOPMENT BUILD";
#endif

            Window.AllowUserResizing = true;

            GameValues.GameContent = Content;
            GameValues.GraphicsDevice = GraphicsDevice;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            GameValues.SpriteBatch = _spriteBatch;

            DefaultContent.LoadContent(Content);

            Setup();
        }

        private void Setup()
        {
            // Screen Size
            Window.ClientSizeChanged += (object sender, EventArgs e) =>
            GameEvents.OnScreenSizeChange?.Invoke(
                _graphics.PreferredBackBufferWidth,
                _graphics.PreferredBackBufferHeight);
            GameEvents.OnScreenSizeChange += (int width, int height) =>
            {
                GameValues.ScreenSize.X = width;
                GameValues.ScreenSize.Y = height;
            };

            // Manual invoke of screen size change to get initial screen size
            GameEvents.OnScreenSizeChange?.Invoke(
                _graphics.PreferredBackBufferWidth,
                _graphics.PreferredBackBufferHeight);

            _mainCamera = new Camera(_spriteBatch);
            ObjectBin.RegisterObject(GameConstants.MAIN_CAMERA, _mainCamera);

            SceneManager.Init(Content, this);
            DrawManager.Setup(GraphicsDevice);

#if DEVELOPMENT            
            InputProvider.RegisterActionToKeyTap(Keys.F3, () => { SceneManager.ToggleScene(GameConstants.SceneNames.DevMenu); });
#endif
        }

        protected override void Update(GameTime gameTime)
        {
            //TODO - Remove this - keep it for now just incase tho
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            InputProvider.CheckInputs();

            SceneManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            DrawManager.PreDraw();
            DrawManager.Draw(_spriteBatch, GraphicsDevice, gameTime);

            base.Draw(gameTime);
        }
    }
}
