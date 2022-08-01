using UnityEditor;
using UnityEngine;
using Object = System.Object;

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

        #region OverrideOperator
        
        public static bool operator !(OzComponent<TClass> component1)
        {
            return ReferenceEquals(component1, null);
        }
        
        public static bool operator false(OzComponent<TClass> component1)
        {
            return ReferenceEquals(component1, null);
        }
        
        public static bool operator true(OzComponent<TClass> component1)
        {
            return !ReferenceEquals(component1, null);
        }
        
        public static TClass operator + (OzComponent<TClass> component, OzEditorObject editorObject)
        {
            return editorObject.AddComponent<TClass>();
        }
        
        public static TClass operator + (OzComponent<TClass> component, OzEditorParam param)
        {
            return param.editorObject.AddComponent<TClass>(param.drawGUI);
        }

        #endregion
    }
}
