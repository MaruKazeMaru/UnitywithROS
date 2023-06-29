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

    private enum TopicType { Pub, Sub };

    private string pkg;
    private string msg;
    private string topic;
    private TopicType pubsub = TopicType.Sub;
    private float frequency = 1f;
    private int queueSize = 1;

    private void OnGUI()
    {
        this.pkg = EditorGUILayout.TextField("package name", this.pkg);
        this.msg = EditorGUILayout.TextField("message name", this.msg);
        this.topic = EditorGUILayout.TextField("topic name", this.topic);
        EditorGUILayout.LabelField("You can change topic name after create.");
        this.pubsub = (TopicType)EditorGUILayout.EnumPopup("pub or sub", this.pubsub);

        if(this.pubsub == TopicType.Pub)
        {
            EditorGUILayout.LabelField("publish config");
            this.frequency = EditorGUILayout.FloatField("frequency", this.frequency);
            this.queueSize = EditorGUILayout.IntField("queue size", this.queueSize);
        }

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
                switch (this.pubsub)
                {
                    case TopicType.Pub:
                        var pub = obj.AddComponent<ROSPub>();
                        pub.topicName = this.topic;
                        pub.frequency = this.frequency;
                        pub.queueSize = this.queueSize;                        
                        break;
                    case TopicType.Sub:
                        var sub = obj.AddComponent<ROSSub>();
                        sub.topicName = this.topic;
                        break;
                    default:
                        goto case TopicType.Sub;
                }
                CreateObj(obj, msgType);

                Undo.RegisterCreatedObjectUndo(obj, "create new topic obj");

                EditorGUILayout.LabelField("Creadted " + this.topic);
            }
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
            i.Init(type);

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