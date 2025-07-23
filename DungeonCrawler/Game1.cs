using DungeonCrawler.Code.Input;
using DungeonCrawler.Code.Scenes.Instances;
using DungeonCrawler.Code.Scenes;
using DungeonCrawler.Code.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using DungeonCrawler.Code.UI;

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
            Window.AllowUserResizing = true;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

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

            // Scene Manager (the first scene added will be the default scene)
            SceneManager.AddedScenes.Add(GameConstants.MainMenu, new Scene_MainMenu(_spriteBatch));
            SceneManager.AddedScenes.Add(GameConstants.Game, new Scene_Game(_spriteBatch));
            SceneManager.Init(Content, this);

            //Content
            GameValues.GameContent = Content;

            _mainCamera = new Camera(_spriteBatch);
            ObjectBin.RegisterObject(GameConstants.MAIN_CAMERA, _mainCamera);
        }

        protected override void Update(GameTime gameTime)
        {
            //TODO - Remove this - keep it for now just incase tho
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            InputProvider.CheckInputs();

            // Update State Manager
            SceneManager.UpdateScene(Content, this);
            SceneManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            SceneManager.Draw(gameTime, _mainCamera);

#if DEVELOPMENT
            _spriteBatch.Begin();
            _spriteBatch.DrawString(DefaultContent.DefaultFont, "DEVELOPMENT BUILD", new Vector2(5, _graphics.PreferredBackBufferHeight - 20), Color.White);
            _spriteBatch.End();
#endif
            base.Draw(gameTime);
        }
    }
}
