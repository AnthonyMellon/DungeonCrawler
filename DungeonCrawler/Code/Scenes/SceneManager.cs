using DungeonCrawler.Code.Scenes.Instances;
using DungeonCrawler.Code.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;

namespace DungeonCrawler.Code.Scenes
{
    internal static class SceneManager
    {
        public static void Init(ContentManager content, Game game)
        {
            _contentManager = content;
            _game = game;

            if (_defaultScene == null)
            {
                throw new Exception("No default scene :(");
            }

            // Default to the first scene
            _activeScenes.Add(_defaultScene);
            _activeScenes[0].IsEnabled = true;
            _activeScenes[0].DoInit(_contentManager, _game);
        }

        public static void QueueSceneToLoad(string sceneName)
        {
            if (_activeScenes.Contains(Scenes[sceneName])) return;

            _scenesToUnload.Remove(Scenes[sceneName]);
            _scenesToLoad.Add(Scenes[sceneName]);
        }

        public static void QueueSceneToUnload(string sceneName)
        {
            _scenesToLoad.Remove(Scenes[sceneName]);
            _scenesToUnload.Add(Scenes[sceneName]);
        }

        public static void Update(GameTime gametime)
        {
            DeActivateScens();
            ActivateScenes();

            for (int i = 0; i < _activeScenes.Count; i++)
            {
                _activeScenes[i].DoUpdate(gametime);
            }
        }

        private static Dictionary<string, Scene> Scenes = new Dictionary<string, Scene>()
        {
            { GameConstants.SceneNames.MainMenu, new Scene_MainMenu() },
            { GameConstants.SceneNames.Game, new Scene_Game() }
        };
        private static Scene _defaultScene = Scenes[GameConstants.SceneNames.MainMenu];

        private static List<Scene> _activeScenes = new List<Scene>();
        private static List<Scene> _scenesToLoad = new List<Scene>();
        private static List<Scene> _scenesToUnload = new List<Scene>();

        private static ContentManager _contentManager;
        private static Game _game;

        private static void DeActivateScens()
        {
            for (int i = 0; i < _scenesToUnload.Count; i++)
            {
                Scene currentScene = _scenesToUnload[i];

                _activeScenes.Remove(currentScene);

                currentScene.IsEnabled = false;
            }
            _scenesToUnload.Clear();
        }

        private static void ActivateScenes()
        {
            for (int i = 0; i < _scenesToLoad.Count; i++)
            {
                Scene currentScene = _scenesToLoad[i];

                if (!currentScene.Initialised) currentScene.DoInit(_contentManager, _game);
                _activeScenes.Add(currentScene);

                currentScene.IsEnabled = true;
            }
            _scenesToLoad.Clear();
        }
    }
}
