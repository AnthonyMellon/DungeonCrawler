﻿using DungeonCrawler.Code.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DungeonCrawler.Code.Scenes
{
    internal static class SceneManager
    {
        public static Dictionary<string, Scene> AddedScenes = new Dictionary<string, Scene>();

        private static Scene _currentScene;
        private static Scene _nextScene;

        public static void Init(ContentManager content, Game game)
        {
            if (AddedScenes.ElementAt(0).Value == null)
            {
                throw new Exception("No default scene :(");
            }

            // Default to the first scene
            _currentScene = AddedScenes.ElementAt(0).Value;
            _currentScene.IsEnabled = true;
            _currentScene.DoInit(content, game);
        }

        /// <summary>
        /// Set the next scene to be loaded
        /// </summary>
        /// <param name="newScene">The next scene to be loaded</param>
        public static void SetNextScene(string newScene)
        {
            _nextScene = AddedScenes[newScene];
        }

        /// <summary>
        /// Update the current scene to <see cref="_nextScene"/>
        /// </summary>
        /// <param name="content"></param>
        /// <param name="game"></param>
        public static void UpdateScene(ContentManager content, Game game)
        {
            if (_nextScene == null) return;

            _currentScene.IsEnabled = false;
            _currentScene = _nextScene;
            _nextScene = null;

            if (!_currentScene.Initialised) _currentScene.DoInit(content, game);
            _currentScene.IsEnabled = true;

        }

        public static Scene GetCurrentScene()
        {
            return _currentScene;
        }

        public static void Update(GameTime gametime)
        {
            _currentScene.DoUpdate(gametime);
        }

        public static void Draw(GameTime gametime, Camera camera, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {
            _currentScene.DoPreDraw(spriteBatch);
            _currentScene.DoDraw(gametime, camera);
        }
    }
}
