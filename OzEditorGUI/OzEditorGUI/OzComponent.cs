using System;
using System.Reflection;
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

    public abstract partial class OzComponent<TClass> : IOzComponent where TClass : OzComponent<TClass>, new()
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
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                GUILayout.Label(GetName(),EditorStyles.foldoutHeader);
                ReflectGUI();
                OnDrawGUI();
                EditorGUILayout.EndVertical();
            }
        }

        void ReflectGUI()
        {
            Type type = typeof(TClass);
            foreach (var info in type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Default))
            {
                foreach (var attribute in info.GetCustomAttributes(true))
                {
                    if (attribute is OzAttribute oa)
                    {
                        oa.guiReflector.DrawGUI(info,this);
                    }
                }
            }
            
            foreach (var info in type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Default))
            {
                foreach (var attribute in info.GetCustomAttributes(true))
                {
                    if (attribute is OzAttribute oa)
                    {
                        oa.guiReflector.DrawGUI(info,this);
                    }
                }
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
