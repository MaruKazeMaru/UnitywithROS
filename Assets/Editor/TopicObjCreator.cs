#if UNITY_EDITOR

using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TopicObjCreator : EditorWindow
{
    [MenuItem("Robotics/Create TopicObject")]
    static void Open()
    {
        var window = EditorWindow.CreateInstance<TopicObjCreator>();
        window.ShowUtility();
    }

    private string pkg;
    private string msg;
    private string topic;

    private void OnGUI()
    {
        this.pkg = EditorGUILayout.TextField("package name", this.pkg);
        this.msg = EditorGUILayout.TextField("message name", this.msg);
        this.topic = EditorGUILayout.TextField("topic name", this.topic);
        EditorGUILayout.LabelField("You can change topic name after create.");
        if (GUILayout.Button("Create"))
        {
            Type msgType = MessageInfoGetter.GetType(pkg, msg);
            if(msgType == null)
            {
                Debug.LogError(pkg + "/" + msg + " does NOT exist.");
            }
            else
            {
                string objName;
                if (topic == "")
                    objName = "new topic";
                else
                    objName = topic;
                var obj = new GameObject(objName);
                obj.AddComponent<ROSTopic>().topicName = this.topic;
                CreateObj(obj, msgType);

                Undo.RegisterCreatedObjectUndo(obj, "create new topic obj");

                this.Close();
            }
        }

        if (GUILayout.Button("Canncel"))
        {
            this.Close();
        }
    }

    private void CreateObj(GameObject obj, Type type)
    {
        if (type == typeof(double))
            obj.AddComponent<ROSBuiltinFloat64>();
        else if (type == typeof(float))
            obj.AddComponent<ROSBuiltinFloat32>();
        else
        {
            var i = obj.AddComponent<ROSInterface>();
            FieldInfo k_name = type.GetField("k_RosMessageName");
            i.SetMessageName((string)k_name.GetValue(type));

            FieldInfo[] fieldInfos = MessageInfoGetter.GetFieldInfos(type);
            foreach(FieldInfo fieldInfo in fieldInfos)
            {
                var child = new GameObject(fieldInfo.Name);
                child.transform.parent = obj.transform;
                CreateObj(child, fieldInfo.FieldType);
            }
        }
    }
}

#endif