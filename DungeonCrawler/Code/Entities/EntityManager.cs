using DungeonCrawler.Code.Entities.Enemies;
using DungeonCrawler.Code.Scenes;
using System.Collections.Generic;

namespace DungeonCrawler.Code.Entities
{
    internal class EntityManager : Dynamic
    {
        public Player Player { get; private set; }
        public List<Entity> Enemies { get; private set; } = new List<Entity>();

        public EntityManager(Scene scene, bool enabled = true) :
            base(enabled)
        {
            _scene = scene;
        }

        /// <summary>
        /// Build a new player and destroy the old one
        /// </summary>
        public void BuildPlayer()
        {
            // Destroy the old player (if there is one)
            Player?.Destroy();

            // Build the new player
            Player = AddChild(new Player(this, _scene)) as Player;
        }

        /// <summary>
        /// Create and register a new enemy
        /// </summary>
        public Entity BuildNewEnemy()
        {
            BasicEnemy enemy = RegisterNewEnemy(new BasicEnemy(this, _scene)) as BasicEnemy;
            return enemy;
        }

        private Scene _scene;

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
