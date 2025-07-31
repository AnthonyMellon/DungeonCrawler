using DungeonCrawler.Code.UI;
using DungeonCrawler.Code.UI.Utils;
using DungeonCrawler.Code.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonCrawler.Code.Scenes.Instances
{
    internal class Scene_MainMenu : Scene
    {
        #region publics
        public override void Init(ContentManager content, Game game)
        {
            _content = content;
            _game = game;

            LoadContent();
            BuildUI();
        }
        #endregion

        #region privates
        private Game _game;
        private ContentManager _content;
        private Texture2D _background;

        private void LoadContent()
        {
            _background = _content.Load<Texture2D>(GameConstants.MENU_BACKGROUND_PATH);
        }

        private void BuildUI()
        {
            UI_Panel basePanel = AddChild(
                new UI_Panel(
                    _background,
                    Color.White,
                    GameConstants.GameLayers.Bottom,
                    AnchorPoints.Fill,
                    Size.Zero,
                    Offset.Zero,
                    DynamicRectangle.FitTypes.Screen,
                    DynamicRectangle.GrowFroms.Edges,
                    this
                    )) as UI_Panel;

            basePanel.AddChild(BuildMenuButtons());
        }

        private UI_Panel BuildMenuButtons()
        {
            UI_Panel menuButtonPanel = new UI_Panel(
                DefaultContent.DefaultRectangle,
                new Color(Color.Gray, 100),
                GameConstants.GameLayers.UI_Background,
                AnchorPoints.LeftStretch,
                new Size(300, 0),
                Offset.Zero,
                DynamicRectangle.FitTypes.Parent,
                DynamicRectangle.GrowFroms.Auto,
                this);

            UI_Button playButton = menuButtonPanel.AddChild(
                new UI_Button(
                    Color.White,
                    Color.Yellow,
                    "Play Game",
                    GameConstants.GameLayers.UI_Foregound,
                    AnchorPoints.Center,
                    new Size(256, 64),
                    new Offset(-16, -40),
                    DynamicRectangle.FitTypes.Parent,
                    DynamicRectangle.GrowFroms.Auto,
                    this
                )) as UI_Button;
            playButton.OnClick += EnterGame;

            UI_Button quitButton = menuButtonPanel.AddChild(
                new UI_Button(
                    Color.White,
                    Color.Yellow,
                    "Quit Game",
                    GameConstants.GameLayers.UI_Foregound,
                    AnchorPoints.Center,
                    new Size(256, 64),
                    new Offset(-16, 40),
                    DynamicRectangle.FitTypes.Parent,
                    DynamicRectangle.GrowFroms.Auto,
                    this
                )) as UI_Button;
            quitButton.OnClick += _game.Exit;

            return menuButtonPanel;
        }

        private void EnterGame()
        {
            SceneManager.LoadSceneNonAdditive(GameConstants.SceneNames.Game);
        }

        protected override void Update(GameTime gametime) { }
        #endregion
    }
}
