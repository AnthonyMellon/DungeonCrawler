using DungeonCrawler.Code.Dungeons;
using DungeonCrawler.Code.Entities;
using DungeonCrawler.Code.Entities.Enemies;
using DungeonCrawler.Code.UI;
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
            //_currentDungeon.Enter();
        }

        public Scene_Game(SpriteBatch graphics) :
            base(graphics)
        {

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
                    anchorPoints: new Vector4(0f, 1f, 0f, 1f),
                    padding: new Point4(0, 0, 0, 0),
                    offset: new Point(0, 0),
                    fitType: UIComponent.FitTypes.Screen
                )) as UI_Panel;

            basePanel.AddChild(BuildMenuBar());
        }

        private UI_Panel BuildMenuBar()
        {
            UI_Panel menuBar = new UI_Panel (
                anchorPoints: new Vector4(0f, 1f, 0f, 0f),
                padding: new Point4(0, 0, 0, 50),
                offset: new Point(0, 0),
                texture: DefaultContent.DefaultRectangle,
                color: new Color(Color.Gray, 100),
                layer: GameConstants.GameLayers.UI_Texutre);

            UI_Button menuButton = menuBar.AddChild(
                new UI_Button
                (
                    anchorPoints: new Vector4(1f, 1f, 0.5f, 0.5f),
                    padding: new Point4(160, 0, 20, 20),
                    offset: new Point(-10, 0),
                    baseColor: Color.White,
                    hoverColor: Color.Yellow,
                    null,
                    "Main Menu",
                    null
                )) as UI_Button;
            menuButton.OnClick += QuitToMainMenu;

            return menuBar;
        }

        private void QuitToMainMenu()
        {
            SceneManager.SetNextScene(GameConstants.MainMenu);
        }

        protected override void Draw(GameTime gametime, Camera camera) { }

        protected override void Update(GameTime gametime) { }
        #endregion
    }
}
