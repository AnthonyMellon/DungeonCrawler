using DungeonCrawler.Code.UI;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Diagnostics;

namespace DungeonCrawler.Code
{
    internal abstract class Dynamic
    {
        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }
            set
            {
                _isEnabled = value;

                if (value == true) OnEnable();
                else OnDisable();
            }
        }
        private bool _isEnabled;

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

        public Dynamic(bool enabled)
        {
            IsEnabled = enabled;
        }        

        /// <summary>
        /// The update method to be called by this Dynamics parent
        /// </summary>
        /// <param name="gametime"></param>
        public void DoUpdate(GameTime gametime)
        {
            if (!IsEnabled) return;

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

        public virtual void DoDraw(GameTime gametime, Camera camera)
        {
            if (!IsEnabled) return;

            Draw(gametime, camera);

            // Draw all children
            for (int i = 0; i < Children.Count; i++)
            {
                Dynamic child = Children[i];
                if (child == null) continue;

                child.DoDraw(gametime, camera);
            }
        }
        protected abstract void Draw(GameTime gametime, Camera camera);

        public Dynamic AddChild(Dynamic child)
        {
            Children.Add(child);
            child.OnDestroy += RemoveChild;
            child.Parent = this;
            return child;
        }

        public void AddChildren(List<Dynamic> children)
        {
            for (int i = 0; i < children.Count; i++)
            {
                AddChild(children[i]);
            }
        }

        public void RemoveChild(Dynamic child)
        {
            if (!Children.Contains(child)) return;

            child.OnDestroy -= RemoveChild;
            Children.Remove(child);
        }

        public void DestroyChildren()
        {
            for (int i = Children.Count - 1; i >= 0; i--)
            {
                Children[i].Destroy();
            }
        }

        public void Destroy()
        {
            OnDestroy?.Invoke(this);
            DestroyChildren();
            Parent = null;
        }

        protected virtual void OnParentSet(Dynamic oldParent, Dynamic newParent) { }
        protected virtual void OnEnable() { }
        protected virtual void OnDisable() { }
    }
}
