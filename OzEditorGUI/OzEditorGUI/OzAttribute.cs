using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Object = System.Object;

namespace OzEditorGUI
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Method)]
    public class OzAttribute : Attribute
    {
        public OzGUIReflector guiReflector;
        public OzAttribute() {}
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class OzFloatField : OzAttribute
    {
        public OzFloatField()
        {
            guiReflector = new OzFloatFieldReflector();
        }
    }
    
    [AttributeUsage(AttributeTargets.Field)]
    public class OzTextField : OzAttribute
    {
        public OzTextField()
        {
            guiReflector = new OzTextFieldReflector();
        }
    }
    
    [AttributeUsage(AttributeTargets.Method)]
    public class OzButton : OzAttribute
    {
        public OzButton()
        {
            guiReflector = new OzButtonRelector();
        }
    }

    public class OzGUIReflector
    {
        public virtual void DrawGUI(MemberInfo info, Object obj) { }
    }
    
    public class OzFloatFieldReflector :OzGUIReflector
    {
        public override void DrawGUI(MemberInfo info, Object obj)
        {
            if (info is FieldInfo fieldInfo)
            {
                var a = EditorGUILayout.FloatField(fieldInfo.Name, (float)fieldInfo.GetValue(obj));
                fieldInfo.SetValue(obj,a);
            }
        }
    }
    
    public class OzTextFieldReflector :OzGUIReflector
    {
        public override void DrawGUI(MemberInfo info, Object obj)
        {
            if (info is FieldInfo fieldInfo)
            {
                var a = EditorGUILayout.TextField(fieldInfo.Name, (string)fieldInfo.GetValue(obj));
                fieldInfo.SetValue(obj,a);
            }
        }
    }

    public class OzButtonRelector : OzGUIReflector
    {
        public override void DrawGUI(MemberInfo info, object obj)
        {
            if (info is MethodInfo memberInfo)
            {
                if (GUILayout.Button(memberInfo.Name))
                {
                    object[] param = new object[]{};
                    memberInfo.Invoke(obj,param);
                }
            }
        }
    }
    
}