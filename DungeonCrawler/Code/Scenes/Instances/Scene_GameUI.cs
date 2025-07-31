using DungeonCrawler.Code.UI;
using DungeonCrawler.Code.UI.Utils;
using DungeonCrawler.Code.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace DungeonCrawler.Code.Scenes.Instances
{
    internal class Scene_GameUI : Scene
    {
        public override void Init(ContentManager content, Game game)
        {
            BuildUI();
        }

        private void BuildUI()
        {
            UI_Panel basePanel = AddChild(
            new UI_Panel(
                DefaultContent.DefaultRectangle,
                Color.Transparent,
                GameConstants.GameLayers.Bottom,
                AnchorPoints.Fill,
                Size.Zero,
                Offset.Zero,
                DynamicRectangle.FitTypes.Screen,
                DynamicRectangle.GrowFroms.Edges,
                this
                )) as UI_Panel;

            basePanel.AddChild(BuildMenuBar());
        }

        private UI_Panel BuildMenuBar()
        {
            UI_Panel menuBar = new UI_Panel(
                DefaultContent.DefaultRectangle,
                new Color(Color.Gray, 100),
                GameConstants.GameLayers.UI_Background,
                AnchorPoints.TopStretch,
                new Size(0, 100),
                Offset.Zero,
                DynamicRectangle.FitTypes.Parent,
                DynamicRectangle.GrowFroms.Auto,
                this);

            UI_Button menuButton = menuBar.AddChild(
            new UI_Button(
                Color.White,
                Color.Yellow,
                "Main Menu",
                GameConstants.GameLayers.UI_Foregound,
                AnchorPoints.CenterRight,
                new Size(256, 64),
                new Offset(-10, 0),
                DynamicRectangle.FitTypes.Parent,
                DynamicRectangle.GrowFroms.Auto,
                this
            )) as UI_Button;
            menuButton.OnClick += QuitToMainMenu;

            return menuBar;
        }

        private void QuitToMainMenu()
        {
            SceneManager.LoadSceneNonAdditive(GameConstants.SceneNames.MainMenu);
        }
    }
}
