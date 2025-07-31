using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace DungeonCrawler.Code.Scenes
{
    internal abstract class Scene : Dynamic
    {
        public bool Initialised { get; private set; }

        /// <summary>
        /// To be called when the scene is loaded
        /// </summary>
        /// <param name="content"></param>
        /// <param name="game"></param>
        public void DoInit(ContentManager content, Game game)
        {
            Initialised = false;
            Init(content, game);
            Initialised = true;
        }

        /// <summary>
        /// Called when the scene is loaded
        /// </summary>
        /// <param name="content"></param>
        /// <param name="game"></param>
        public abstract void Init(ContentManager content, Game game);

        public Scene(bool enabled = false) :
            base(enabled)
        {
        }
    }
}
