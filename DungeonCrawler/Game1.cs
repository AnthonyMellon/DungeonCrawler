using DungeonCrawler.Code.DrawManagement;
using DungeonCrawler.Code.Input;
using DungeonCrawler.Code.Scenes;
using DungeonCrawler.Code.Scenes.Instances;
using DungeonCrawler.Code.UI;
using DungeonCrawler.Code.UI.Utils;
using DungeonCrawler.Code.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace DungeonCrawler
{
    public class Game1 : Game
    {

#if DEVELOPMENT
        private UI_Panel _developmentPanel;
#endif

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

#if DEVELOPMENT
            BuildDevelopmentStuff();
#endif

            // Manual invoke of screen size change to get initial screen size
            GameEvents.OnScreenSizeChange?.Invoke(
                _graphics.PreferredBackBufferWidth,
                _graphics.PreferredBackBufferHeight);

            // Scene Manager (the first scene added will be the default scene)
            SceneManager.AddedScenes.Add(GameConstants.MainMenu, new Scene_MainMenu());
            SceneManager.AddedScenes.Add(GameConstants.Game, new Scene_Game());
            SceneManager.Init(Content, this);

            _mainCamera = new Camera(_spriteBatch);
            ObjectBin.RegisterObject(GameConstants.MAIN_CAMERA, _mainCamera);

            DrawManager.Setup(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            //TODO - Remove this - keep it for now just incase tho
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            InputProvider.CheckInputs();

#if DEVELOPMENT
            _developmentPanel?.DoUpdate(gameTime);
#endif

            // Update Scene Manager
            SceneManager.UpdateScene(Content, this);
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

        private void BuildDevelopmentStuff()
        {
            _developmentPanel = new UI_Panel(
                AnchorPoints.Center,
                Padding.Positive * 250,
                Offset.Zero,
                null,
                new Color(0, 255, 0, 150),
                GameConstants.GameLayers.Bottom,
                DrawManager.DrawTargets.Development,
                UIComponent.FitTypes.Screen);

            UI_Text developmentBuildText = new UI_Text(
                "Development Build",
                Color.White,
                AnchorPoints.BottomLeft,
                Padding.Zero,
                Offset.Zero,
                GameConstants.GameLayers.Top,
                DrawManager.DrawTargets.Development,
                UIComponent.FitTypes.Parent);
            _developmentPanel.AddChild(developmentBuildText);

            UI_Button testButton = new UI_Button(
                AnchorPoints.BottomRight,
                new Padding(128, 32),
                new Offset(-128, -32),
                new Color(255, 0, 0),
                new Color(0, 255, 0),
                "Test Button",
                DrawManager.DrawTargets.Development,
                UIComponent.FitTypes.Parent);
            //_developmentPanel.AddChild(testButton);
        }
    }
}
