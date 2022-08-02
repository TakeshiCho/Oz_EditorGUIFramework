using System;
using System.Collections.Generic;
using OzEditorGUIRuntime;
using UnityEditor;
using UnityEngine;
using Object = System.Object;

namespace OzEditorGUI
{
    public abstract partial class OzEditorObject : EditorWindow
    {
        private List<IOzComponent> _components = new List<IOzComponent>();
        
        private OzGizmosComponent _gizmosComponent;

        public T AddComponent<T>(bool drawGUI = true) where T : OzComponent<T>, new()
        {
            T component = OzComponent<T>.Init(this, drawGUI);
            _components.Add(component);
            return component;
        }

        public T GetComponet<T>() where T : OzComponent<T>, new()
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

        public bool TryGetComponet<T>(out T component) where T : OzComponent<T>, new()
        {
            foreach (var cp in _components)
            {
                if (cp is T target)
                {
                    component = target;
                    return true;
                }
            }

            component = null;
            return false;
        }

        public List<T> GetComponets<T>() where T : OzComponent<T>, new()
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

        public bool TryGetComponets<T>(out List<T> components) where T : OzComponent<T>, new()
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

        private void OnEnable()
        {
            OnEditorInit();
            InitGizmosComponent();
        }

        void InitGizmosComponent()
        {
            if (!_gizmosComponent)
            {
                GameObject ob = new GameObject();
                ob.name = $"{GetType().ToString()} Gizmos Component";
                ob.hideFlags = HideFlags.NotEditable;
                _gizmosComponent = ob.AddComponent<OzGizmosComponent>();
                _gizmosComponent.drawGizmos += DrawGizmos;
            }
        }
        
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
                cp.DrawGizmos();
            }
        }

        private void OnGUI()
        {
            OnEditorGUI();
            InitGizmosComponent();
            
        }

        public void OnDestroy()
        {
            if (_gizmosComponent != null)
            {
                _gizmosComponent.drawGizmos -= DrawGizmos;
                _gizmosComponent.DestroySelf();
            }
            foreach (var cp in _components)
            {
                cp.OnDestroy();
            }
            OnEditorDestroy();
        }
        public abstract void OnEditorInit();
        public abstract void OnEditorGUI();
        public virtual void OnEditorDestroy(){}
        
    }
}