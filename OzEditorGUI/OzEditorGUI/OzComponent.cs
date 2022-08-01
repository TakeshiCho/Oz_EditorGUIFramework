using UnityEditor;
using UnityEngine;

namespace OzEditorGUI
{ 
    public interface IOzComponent
    {
        string GetName();
        bool CannotOperate();
        void Initialize();
        void DrawGUI();
        void DrawGizmos();
        void OnDestroy();
    }

    public abstract class OzComponent<TClass> : IOzComponent where TClass : OzComponent<TClass>, new()
    {
        public static TClass Init(OzEditorObject editorObject, bool drawGUI)
        {
            TClass component = new TClass();
            component._drawGUI = drawGUI;
            component._editorObject = editorObject;
            component.Initialize();
            return component;
        }
        
        private bool _drawGUI;
        private OzEditorObject _editorObject;

        public abstract void Initialize();
        public abstract string GetName();
        public abstract bool CannotOperate();

        public void DrawGUI()
        {
            if (_drawGUI)
            {
                GUILayout.Label(GetName(),EditorStyles.foldoutHeader);
                OnDrawGUI();
            }
        }
        
        public abstract void OnDrawGUI();

        public void DrawGizmos()
        {
            OnDrawGizmos();
        }
        public virtual void OnDrawGizmos() {}
        public virtual void OnDestroy(){}
        
    }
}
