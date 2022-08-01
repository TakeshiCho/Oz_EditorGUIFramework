using System.Collections.Generic;

namespace OzEditorGUI
{
    public abstract class OzEditorObject
    {
        protected List<IOzComponent> _components = new List<IOzComponent>();
        
        public T AddComponent<T>(bool drawGUI = true) where T: OzComponent<T>, new()
        {
            T component = OzComponent<T>.Init(drawGUI);
            _components.Add(component);
            return component;
        }

        public T GetComponet<T>() where T: OzComponent<T>, new()
        {
            foreach (var cp in _components)
            {
                if (cp is T target)
                {
                    return target;
                }
            }
            return null;
        }
        
        public bool TryGetComponet<T>(out T component) where T: OzComponent<T>, new()
        {
            foreach (var cp in _components)
            {
                if (cp is T target)
                {
                    component =  target;
                    return true;
                }
            }
            component = null;
            return false;
        }
        
        public List<T> GetComponets<T>() where T: OzComponent<T>, new()
        {
            List<T> cp = new List<T>();
            foreach (var component in _components)
            {
                if (component is T target)
                {
                    cp.Add(target);
                }
            }
            return cp;
        }
        
        public bool TryGetComponets<T>(out List<T> components) where T: OzComponent<T>, new()
        {
            bool validate = false;
            components = new List<T>();
            foreach (var cp in _components)
            {
                if (cp is T target)
                {
                    components.Add(target);
                    validate = true;
                }
            }
            return validate;
        }

        public abstract void Init();

        public void DrawGUILayout()
        {
            foreach (var cp in _components)
            {
                cp.DrawGUI();
            }
        }
        
        public void DrawGizmos()
        {
            foreach (var cp in _components)
            {
                cp.DrawGizoms();
            }
        }

        public void Destroy()
        {
            foreach (var cp in _components)
            {
                cp.Destroy();
            }
        }
    }
}