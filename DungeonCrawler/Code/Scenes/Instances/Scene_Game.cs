//using DungeonCrawler.Code.DebugTools;
using DungeonCrawler.Code.DrawManagement;
using DungeonCrawler.Code.Dungeons;
using DungeonCrawler.Code.Entities;
using DungeonCrawler.Code.Entities.Enemies;
using DungeonCrawler.Code.UI;
using DungeonCrawler.Code.UI.Utils;
using DungeonCrawler.Code.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

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
                new UI_Panel
                (
                    anchorPoints: AnchorPoints.Fill,
                    padding: Padding.Zero,
                    offset: Offset.Zero,
                    fitType: UIComponent.FitTypes.Screen
                )) as UI_Panel;

            basePanel.AddChild(BuildMenuBar());
        }

        private UI_Panel BuildMenuBar()
        {
            UI_Panel menuBar = new UI_Panel(
                anchorPoints: AnchorPoints.TopStretch,
                padding: new Padding(0, 0, 0, 50),
                offset: Offset.Zero,
                texture: DefaultContent.DefaultRectangle,
                color: new Color(Color.Gray, 100),
                layer: GameConstants.GameLayers.UI_Texutre);

            UI_Button menuButton = menuBar.AddChild(
                new UI_Button
                (
                    anchorPoints: AnchorPoints.CenterRight,
                    padding: new Padding(160, 0, 20, 20),
                    offset: new Offset(-10, 0),
                    baseColor: Color.White,
                    hoverColor: Color.Yellow,
                    "Main Menu",
                    DrawManager.DrawTargets.UI,
                    UIComponent.FitTypes.Parent
                )) as UI_Button;
            menuButton.OnClick += QuitToMainMenu;

            return menuBar;
        }

        private void QuitToMainMenu()
        {
            SceneManager.SetNextScene(GameConstants.MainMenu);
        }

        protected override void Update(GameTime gametime) { }
        #endregion
    }
}
