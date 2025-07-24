using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace DungeonCrawler.Code.Utils.Drawables
{
    internal abstract class Drawable
    {
        protected Color Color { get; set; }
        private DrawManager.DrawTargets _drawTarget { get; set; }

        public Action OnChange;
        public bool Visible
        {
            get
            {
                return _visible;
            }
            set
            {
                if (_visible) DrawManager.RegisterDrawable(_drawTarget, this);
                if (!_visible) DrawManager.DeregisterDrawable(_drawTarget, this);

                _visible = value;
            }
        }
        private bool _visible;

        public abstract void Draw(SpriteBatch spritebatch, GameTime gameTime);

        public Drawable(DrawManager.DrawTargets drawTarget, bool visible = true)
        {
            _drawTarget = drawTarget;
            Visible = visible;
        }
    }
}
