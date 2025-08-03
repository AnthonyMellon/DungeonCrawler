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
        public static Dictionary<string, Scene> AddedScenes { get; private set; } = new Dictionary<string, Scene>()
        {
#if DEVELOPMENT
            { GameConstants.SceneNames.DevMenu, new Scene_DevMenu() },
#endif
            { GameConstants.SceneNames.MainMenu, new Scene_MainMenu() },
            { GameConstants.SceneNames.GameUI, new Scene_GameUI() },
            { GameConstants.SceneNames.Game, new Scene_Game() },
        };
        public static List<Scene> ActiveScenes { get; private set; } = new List<Scene>();

        public static void Init(ContentManager content, Game game)
        {
            _contentManager = content;
            _game = game;

            if (_defaultScene == null)
            {
                throw new Exception("No default scene :(");
            }
            LoadScene(_defaultScene);
        }

        public static void ToggleScene(string sceneName)
        {
            if (ActiveScenes.Contains(AddedScenes[sceneName])) ToggleScene(sceneName, false);
            else ToggleScene(sceneName, true);
        }

        public static void ToggleScene(string sceneName, bool toggle)
        {
            if (toggle) QueueSceneToLoad(sceneName);
            else QueueSceneToUnload(sceneName);
        }

        public static void LoadSceneNonAdditive(string sceneName)
        {
            UnloadAllActiveScenes();
            _scenesToLoad.Add(AddedScenes[sceneName]);
        }

        public static void Update(GameTime gametime)
        {
            UnloadScenes();
            LoadScenes();

            for (int i = 0; i < ActiveScenes.Count; i++)
            {
                ActiveScenes[i].DoUpdate(gametime);
            }
        }

        private static Scene _defaultScene = AddedScenes[GameConstants.SceneNames.Game];

        private static List<Scene> _scenesToLoad = new List<Scene>();
        private static List<Scene> _scenesToUnload = new List<Scene>();

        private static ContentManager _contentManager;
        private static Game _game;

        private static void QueueSceneToLoad(string sceneName)
        {
            if (ActiveScenes.Contains(AddedScenes[sceneName])) return;

            _scenesToUnload.Remove(AddedScenes[sceneName]);
            _scenesToLoad.Add(AddedScenes[sceneName]);
        }

        private static void QueueSceneToUnload(string sceneName)
        {
            _scenesToLoad.Remove(AddedScenes[sceneName]);
            _scenesToUnload.Add(AddedScenes[sceneName]);
        }

        private static void UnloadScenes()
        {
            for (int i = 0; i < _scenesToUnload.Count; i++)
            {
                Scene currentScene = _scenesToUnload[i];

                ActiveScenes.Remove(currentScene);

                currentScene.IsEnabled = false;
            }
            _scenesToUnload.Clear();
        }

        private static void LoadScenes()
        {
            for (int i = 0; i < _scenesToLoad.Count; i++)
            {
                LoadScene(_scenesToLoad[i]);
            }
            _scenesToLoad.Clear();
        }

        private static void LoadScene(Scene sceneToLoad)
        {
            if (!sceneToLoad.Initialised) sceneToLoad.DoInit(_contentManager, _game);
            sceneToLoad.OnEnter();
            ActiveScenes.Add(sceneToLoad);

            sceneToLoad.IsEnabled = true;
        }

        private static void UnloadAllActiveScenes()
        {
            for (int i = 0; i < ActiveScenes.Count; i++)
            {
                _scenesToUnload.Add(ActiveScenes[i]);
            }
        }
    }
}
