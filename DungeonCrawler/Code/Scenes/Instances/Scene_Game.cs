using DungeonCrawler.Code.DrawManagement;
using DungeonCrawler.Code.Dungeons;
using DungeonCrawler.Code.Entities;
using DungeonCrawler.Code.Entities.Enemies;
using DungeonCrawler.Code.UI;
using DungeonCrawler.Code.UI.Utils;
using DungeonCrawler.Code.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace DungeonCrawler.Code.Scenes.Instances
{
    internal class Scene_Game : Scene
    {
        #region publics
        public override void Init(ContentManager content, Game game)
        {
            _camera = ObjectBin.GetObject(GameConstants.MAIN_CAMERA) as Camera;

            _entityManager = AddChild(new EntityManager(_camera)) as EntityManager;
            _entityManager.BuildPlayer();

            //TEMP ENEMIES
            BasicEnemy enemy1 = _entityManager.BuildNewEnemy() as BasicEnemy;
            enemy1.WorldPosition = new Point(100, 100);
            BasicEnemy enemy2 = _entityManager.BuildNewEnemy() as BasicEnemy;
            enemy2.WorldPosition = new Point(-100, -200);
            //TEMP ENEMIES
            //
            BuildUI();

            _currentDungeon = AddChild(new Dungeon()) as Dungeon;
            _currentDungeon.BuildDungeon();
        }
        #endregion region

        #region privates
        private Camera _camera;
        private EntityManager _entityManager;
        private Dungeon _currentDungeon;

        private void BuildUI()
        {
            UI_Panel basePanel = AddChild(
                new UI_Panel(
                    DefaultContent.DefaultRectangle,
                    Color.CornflowerBlue,
                    GameConstants.GameLayers.Bottom,
                    AnchorPoints.Fill,
                    Size.Zero,
                    Offset.Zero,
                    DynamicRectangle.FitTypes.Screen,
                    DynamicRectangle.GrowFroms.Edges,
                    DrawManager.DrawTargets.World
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
                DrawManager.DrawTargets.UI);

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
                    DrawManager.DrawTargets.UI
                )) as UI_Button;
            menuButton.OnClick += QuitToMainMenu;

            return menuBar;
        }

        private void QuitToMainMenu()
        {
            SceneManager.ToggleScene(GameConstants.SceneNames.Game, false);
            SceneManager.ToggleScene(GameConstants.SceneNames.MainMenu, true);
        }

        protected override void Update(GameTime gametime) { }
        #endregion
    }
}
