using DungeonCrawler.Code.Dungeons;
using DungeonCrawler.Code.Entities;
using DungeonCrawler.Code.Entities.Enemies;
using DungeonCrawler.Code.UI;
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

            _entityManager = AddChild(new EntityManager(_camera, this)) as EntityManager;
            _entityManager.BuildPlayer();

            //TEMP ENEMIES
            BasicEnemy enemy1 = _entityManager.BuildNewEnemy() as BasicEnemy;
            enemy1.WorldPosition = new Point(100, 100);
            BasicEnemy enemy2 = _entityManager.BuildNewEnemy() as BasicEnemy;
            enemy2.WorldPosition = new Point(-100, -200);
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

        protected override void Update(GameTime gametime) { }
        #endregion
    }
}
