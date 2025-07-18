using DungeonCrawler.Code.UI;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Diagnostics;

namespace DungeonCrawler.Code
{
    internal abstract class Dynamic
    {
        #region publics
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

        public Dynamic Parent
        {
            get
            {
                return _parent;
            }
            private set
            {
                Dynamic oldParent = _parent;
                _parent = value;
                OnParentSet(oldParent, _parent);
            }
        }

        public List<Dynamic> Children { get; private set; } = new List<Dynamic>();

        public delegate void OnDestroyHandler(Dynamic self);
        public OnDestroyHandler OnDestroy;

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

        public Dynamic(bool enabled)
        {
            IsEnabled = enabled;
        }
        #endregion

        #region privates
        private bool _isEnabled;
        private Dynamic _parent;

        protected abstract void Update(GameTime gametime);
        protected abstract void Draw(GameTime gametime, Camera camera);
        protected virtual void OnParentSet(Dynamic oldParent, Dynamic newParent) { }
        protected virtual void OnEnable() { }
        protected virtual void OnDisable() { }
        #endregion

    }
}
