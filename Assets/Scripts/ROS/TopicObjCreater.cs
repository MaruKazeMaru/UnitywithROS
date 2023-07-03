using System;
using System.Reflection;
using UnityEngine;
using Float32MultiArray = RosMessageTypes.Std.Float32MultiArrayMsg;

namespace ROSUnity
{
    public static class TopicObjCreater
    {
        public static void Create(GameObject obj, Type messageType)
        {
            if (messageType == typeof(double) || messageType == typeof(float))
                obj.AddComponent<BuiltinFloat>();
            else if (messageType == typeof(uint) || messageType == typeof(int) || messageType == typeof(long) || messageType == typeof(ulong))
                obj.AddComponent<BuiltinInt>();
            else if (messageType == typeof(string))
                obj.AddComponent<BuiltinString>();
            else if (messageType == typeof(bool))
                obj.AddComponent<BuiltinBool>();
            else if (messageType.IsArray)
            {
                Type elementType = messageType.GetElementType();

                var arr = obj.AddComponent<Array>();
                arr.elementTypeName = elementType.FullName;

                var element = new GameObject("element (0)");
                element.transform.parent = obj.transform;
                Create(element, elementType);
            }
            else
            {
                var i = obj.AddComponent<Interface>();
                i.Init(messageType);

                FieldInfo[] fieldInfos = MessageInfoGetter.GetFieldInfos(messageType);
                foreach (FieldInfo fieldInfo in fieldInfos)
                {
                    var child = new GameObject(fieldInfo.Name);
                    child.transform.parent = obj.transform;
                    Create(child, fieldInfo.FieldType);
                }
            }
        }
    }
}
