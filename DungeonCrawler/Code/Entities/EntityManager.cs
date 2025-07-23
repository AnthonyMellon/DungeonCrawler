using DungeonCrawler.Code.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using DungeonCrawler.Code.Entities.Enemies;

namespace DungeonCrawler.Code.Entities
{
    internal class EntityManager : Dynamic
    {
        private Camera _camera;

        public Player Player { get; private set; }
        public List<Entity> Enemies { get; private set; } = new List<Entity>();

        public EntityManager(Camera camera, bool enabled = true) :
            base(enabled)
        {
            _camera = camera;
        }

        /// <summary>
        /// Build a new player and destroy the old one
        /// </summary>
        public void BuildPlayer()
        {
            // Destroy the old player (if there is one)
            Player?.Destroy();

            // Build the new player
            Player = AddChild(new Player(_camera, this)) as Player;
        }

        /// <summary>
        /// Create and register a new enemy
        /// </summary>
        public Entity BuildNewEnemy()
        {
            BasicEnemy enemy = RegisterNewEnemy(new BasicEnemy(_camera, this)) as BasicEnemy;
            return enemy;
        }

        /// <summary>
        /// Register a new enemy to the enemy list and as a child
        /// </summary>
        /// <param name="enemy">the registered enemy</param>
        /// <returns></returns>
        private Entity RegisterNewEnemy(Entity enemy)
        {
            AddChild(enemy);
            Enemies.Add(enemy);

            return enemy;
        }
    }
}
