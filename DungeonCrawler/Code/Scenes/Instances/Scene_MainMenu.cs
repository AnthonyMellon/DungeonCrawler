using DungeonCrawler.Code.UI;
using DungeonCrawler.Code.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonCrawler.Code.Scenes.Instances
{
    internal class Scene_MainMenu : Scene
    {
        private Game _game;
        private ContentManager _content;
        private Camera _camera;

        // Images
        Texture2D _background;

        public override void Init(ContentManager content, Game game)
        {
            _content = content;
            _game = game;
            _camera = ObjectBin.GetObject("MainCamera") as Camera;

            LoadContent();
            BuildUI();
        }

        private void LoadContent()
        {
            _background = _content.Load<Texture2D>("Images/TempMenuBackground");
        }

        private void BuildUI()
        {
            // Build Components
            UI_Panel basePanel = AddChild(
                new UI_Panel
                (
                    anchorPoints: new Vector4(0f, 1f, 0f, 1f),
                    padding: new Point4(0, 0, 0, 0),
                    offset: new Point(0, 0),
                    texture: _background,
                    color: Color.White
                )) as UI_Panel;

            // Menu Buttons
            UI_Panel buttonPanel = basePanel.AddChild(
                new UI_Panel
                (
                    anchorPoints: new Vector4(0.0f, 0.0f, 0.0f, 1.0f),
                    padding: new Point4(0, 300, 0, 0),
                    offset: new Point(0, 0),
                    texture: DefaultContent.DefaultRectangle,
                    color: new Color(Color.Gray, 100)
                )) as UI_Panel;

            // Play Button
            UI_Button playButton = buttonPanel.AddChild(
                new UI_Button
                (
                    anchorPoints: new Vector4(0.5f, 0.5f, 0.5f, 0.5f),
                    padding: new Point4(128, 128, 32, 32),
                    offset: new Point(-16, -40),
                    baseColor: Color.White,
                    hoverColor: Color.Yellow,
                    null,
                    "Play Game",
                    null
                )) as UI_Button;
            playButton.OnClick += EnterGame;


            // Quit Game Button
            UI_Button quitButton = buttonPanel.AddChild(
                new UI_Button
                (
                    anchorPoints: new Vector4(0.5f, 0.5f, 0.5f, 0.5f),
                    padding: new Point4(128, 128, 32, 32),
                    offset: new Point(-16, 40),
                    baseColor: Color.White,
                    hoverColor: Color.Yellow,
                    null,
                    "Quit Game",
                    null
                )) as UI_Button;
            quitButton.OnClick += _game.Exit;
        }

        private void EnterGame()
        {
            SceneManager.SetNextScene("Game");
        }

        protected override void Draw(GameTime gametime, SpriteBatch graphics) { }

        protected override void Update(GameTime gametime) { }

        public override void OnEnter() { }

        public override void OnExit() { }
    }
}
