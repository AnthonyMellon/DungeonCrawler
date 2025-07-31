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

#if DEVELOPMENT
            BuildDevelopmentStuff();
#endif

            // Manual invoke of screen size change to get initial screen size
            GameEvents.OnScreenSizeChange?.Invoke(
                _graphics.PreferredBackBufferWidth,
                _graphics.PreferredBackBufferHeight);

            _mainCamera = new Camera(_spriteBatch);
            ObjectBin.RegisterObject(GameConstants.MAIN_CAMERA, _mainCamera);

            SceneManager.Init(Content, this);
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
                DefaultContent.DefaultRectangle,
                new Color(0, 255, 0, 150),
                GameConstants.GameLayers.Top,
                AnchorPoints.Center,
                new Size(250, 250),
                Offset.Zero,
                DynamicRectangle.FitTypes.Screen,
                DynamicRectangle.GrowFroms.Auto,
                DrawManager.DrawTargets.Development);

            UI_Panel testPanel = new UI_Panel(
                DefaultContent.DefaultRectangle,
                new Color(255, 0, 0, 150),
                GameConstants.GameLayers.Top,
                AnchorPoints.BottomLeft,
                new Size(50, 50),
                Offset.Zero,
                DynamicRectangle.FitTypes.Parent,
                DynamicRectangle.GrowFroms.Center,
                DrawManager.DrawTargets.Development);
            _developmentPanel.AddChild(testPanel);

            /*            UI_Text developmentBuildText = new UI_Text(
                            "Development Build",
                            Color.White,
                            AnchorPoints.BottomLeft,
                            Size.Zero,
                            Offset.Zero,
                            GameConstants.GameLayers.Top,
                            DrawManager.DrawTargets.Development,
                            UIComponent.FitTypes.Parent);
                        _developmentPanel.AddChild(developmentBuildText);

                        UI_TextInput testTextInput = new UI_TextInput(
                            AnchorPoints.TopLeft,
                            new Size(128, 32),
                            Offset.Zero,
                            DrawManager.DrawTargets.Development,
                            UIComponent.FitTypes.Parent
                            );
                        _developmentPanel.AddChild(testTextInput);*/
        }
    }
}
