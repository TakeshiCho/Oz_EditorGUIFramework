using UnityEditor;

namespace OzEditorGUI
{
    public struct OzEditorParam
    {
        public OzEditorObject editorObject;
        public bool drawGUI;
    }
    
    public abstract partial class OzComponent<TClass> : IOzComponent where TClass : OzComponent<TClass>, new()
    {
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
    }

    public abstract partial class OzEditorObject : EditorWindow
    {
        public static OzEditorParam operator +(OzEditorObject editorObject, bool drawGUI)
        {
            OzEditorParam param = new OzEditorParam();
            param.editorObject = editorObject;
            param.drawGUI = drawGUI;
            return param;
        }
    }
}