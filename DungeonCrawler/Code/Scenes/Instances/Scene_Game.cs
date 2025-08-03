using DungeonCrawler.Code.Dungeons;
using DungeonCrawler.Code.Entities;
using DungeonCrawler.Code.Input;
using DungeonCrawler.Code.UI;
using DungeonCrawler.Code.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using DungeonCrawler.Code.Entities.Enemies;

namespace DungeonCrawler.Code.Scenes.Instances
{
    internal class Scene_Game : Scene
    {
        #region publics
        public override void Init(ContentManager content, Game game)
        {
            _camera = ObjectBin.GetObject(GameConstants.MAIN_CAMERA) as Camera;

            _entityManager = AddChild(new EntityManager(this)) as EntityManager;
            _entityManager.BuildPlayer();

            //TEMP ENEMIES
/*            BasicEnemy enemy1 = _entityManager.BuildNewEnemy() as BasicEnemy;
            enemy1.Position = new Point(100, 100);
            enemy1.MoveSpeed = 3;
            BasicEnemy enemy2 = _entityManager.BuildNewEnemy() as BasicEnemy;
            enemy2.Position = new Point(-100, -200);
            enemy2.MoveSpeed = 2;*/
            //TEMP ENEMIES
            //            

            _currentDungeon = AddChild(new Dungeon(this)) as Dungeon;
            _currentDungeon.BuildDungeon();
        }

        public override void OnEnter()
        {
            SceneManager.ToggleScene(GameConstants.SceneNames.GameUI, true);

            base.OnEnter();
        }

        public override void OnExit()
        {
            SceneManager.ToggleScene(GameConstants.SceneNames.GameUI, false);

            base.OnExit();
        }

        #endregion region

        #region privates
        private Camera _camera;
        private EntityManager _entityManager;
        private Dungeon _currentDungeon;

        private void UpdateCamera()
        {
            TransformMatrix = _camera.TransformToEntity(_entityManager.Player);
            _camera.Zoom((int)InputProvider.MouseScrollDirection);
        }

        protected override void Update(GameTime gametime)
        {
            UpdateCamera();
        }
        #endregion
    }
}
