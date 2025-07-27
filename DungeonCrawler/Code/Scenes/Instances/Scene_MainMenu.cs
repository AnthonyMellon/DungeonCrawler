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

        public Scene_MainMenu(SpriteBatch graphics) :
            base(graphics)
        {

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
                new UI_Panel
                (
                    anchorPoints: AnchorPoints.Fill,
                    padding: new Point4(0, 0, 0, 0),
                    offset: new Point(0, 0),
                    texture: _background,
                    color: Color.White,
                    layer: GameConstants.GameLayers.Bottom,
                    DrawManager.DrawTargets.UI,
                    UIComponent.FitTypes.Screen
                )) as UI_Panel;

            basePanel.AddChild(BuildMenuButtons());
        }

        private UI_Panel BuildMenuButtons()
        {
            UI_Panel menuButtonPanel = new UI_Panel(
                anchorPoints: AnchorPoints.LeftStretch,
                padding: new Point4(0, 300, 0, 0),
                offset: new Point(0, 0),
                texture: DefaultContent.DefaultRectangle,
                color: new Color(Color.Gray, 100),
                drawTarget: DrawManager.DrawTargets.UI);

            UI_Button playButton = menuButtonPanel.AddChild(
                new UI_Button
                (
                    anchorPoints: AnchorPoints.Center,
                    padding: new Point4(128, 128, 32, 32),
                    offset: new Point(-16, -40),
                    baseColor: Color.White,
                    hoverColor: Color.Yellow,
                    "Play Game",
                    DrawManager.DrawTargets.UI,
                    UIComponent.FitTypes.Parent
                )) as UI_Button;
            playButton.OnClick += EnterGame;

            UI_Button quitButton = menuButtonPanel.AddChild(
                new UI_Button
                (
                    anchorPoints: AnchorPoints.Center,
                    padding: new Point4(128, 128, 32, 32),
                    offset: new Point(-16, 40),
                    baseColor: Color.White,
                    hoverColor: Color.Yellow,
                    "Quit Game",
                    DrawManager.DrawTargets.UI,
                    UIComponent.FitTypes.Parent                    
                )) as UI_Button;
            quitButton.OnClick += _game.Exit;

            return menuButtonPanel;
        }

        private void EnterGame()
        {
            SceneManager.SetNextScene(GameConstants.Game);
        }

        protected override void Update(GameTime gametime) { }
        #endregion
    }
}
