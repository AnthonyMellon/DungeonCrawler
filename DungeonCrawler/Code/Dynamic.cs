using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace DungeonCrawler.Code
{
    internal abstract class Dynamic
    {
        /// <summary>
        /// This component and its children should recieve updates and do draws
        /// </summary>
        public bool IsActive = true;

        public delegate void OnDestroyHandler(Dynamic self);
        public OnDestroyHandler OnDestroy;

        public Dynamic Parent
        {
            get
            {
                return b_parent;
            }
            private set
            {
                Dynamic oldParent = b_parent;
                b_parent = value;
                OnParentSet(oldParent, b_parent);
            }
        }
        private Dynamic b_parent;

        public List<Dynamic> Children { get; private set; } = new List<Dynamic>();

        /// <summary>
        /// The update method to be called by this Dynamics parent
        /// </summary>
        /// <param name="gametime"></param>
        public void DoUpdate(GameTime gametime)
        {
            if (!IsActive) return;

            Update(gametime);

            // Update all children
            for (int i = 0; i < Children.Count; i++)
            {
                Dynamic child = Children[i];
                if (child == null) continue;

                child.DoUpdate(gametime);
            }
        }

        /// <summary>
        /// Override this method to add behaviour to the update loop
        /// </summary>
        /// <param name="gametime"></param>
        protected abstract void Update(GameTime gametime);

        /// <summary>
        /// The draw method to be called by this Dynamics parent
        /// </summary>
        /// <param name="gametime"></param>
        /// <param name="graphics"></param>
        public virtual void DoDraw(GameTime gametime, SpriteBatch graphics)
        {
            if (!IsActive) return;

            Draw(gametime, graphics);

            // Draw all children
            for (int i = 0; i < Children.Count; i++)
            {
                Dynamic child = Children[i];
                if (child == null) continue;

                child.DoDraw(gametime, graphics);
            }
        }

        /// <summary>
        /// Override this method to add behaviour to the draw loop
        /// </summary>
        /// <param name="gametime"></param>
        /// <param name="graphics"></param>
        protected abstract void Draw(GameTime gametime, SpriteBatch graphics);

        /// <summary>
        /// Attach a child Dynamic to this Dynamic
        /// </summary>
        /// <param name="child">The child to be attached</param>
        /// <returns></returns>
        public Dynamic AddChild(Dynamic child)
        {
            Children.Add(child);
            child.OnDestroy += RemoveChild;
            child.Parent = this;
            return child;
        }

        /// <summary>
        /// Attach a list of child Dynamics to this component
        /// </summary>
        /// <param name="children">The list of child Dynamics to be attached</param>
        public void AddChildren(List<Dynamic> children)
        {
            for (int i = 0; i < children.Count; i++)
            {
                AddChild(children[i]);
            }
        }

        /// <summary>
        /// Remove a child Dynamic from this Dynamic
        /// </summary>
        /// <param name="child">The child to be removed</param>
        public void RemoveChild(Dynamic child)
        {
            if (!Children.Contains(child)) return;

            child.OnDestroy -= RemoveChild;
            Children.Remove(child);
        }

        /// <summary>
        /// Destroys all children attached to this Dynamic
        /// </summary>
        public void DestroyChildren()
        {
            for (int i = Children.Count - 1; i >= 0; i--)
            {
                Children[i].Destroy();
            }
        }

        /// <summary>
        /// Destroy this Dynamic and perfrom any necessary cleanup
        /// </summary>
        public void Destroy()
        {
            OnDestroy?.Invoke(this);
            DestroyChildren();
            Parent = null;
        }

        protected virtual void OnParentSet(Dynamic oldParent, Dynamic newParent)
        {

        }
    }
}
