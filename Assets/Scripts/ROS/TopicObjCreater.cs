using System;
using System.Reflection;
using UnityEngine;
using Float32MultiArray = RosMessageTypes.Std.Float32MultiArrayMsg;

namespace ROSTopic2GameObject
{
    public static class TopicObjCreater
    {
        public static void Create(GameObject obj, Type messageType)
        {
            if (messageType == typeof(float))
                obj.AddComponent<BuiltinFloat>().bit = 32;
            else if (messageType == typeof(double))
                obj.AddComponent<BuiltinFloat>().bit = 64;

            else if (messageType == typeof(sbyte))
                obj.AddComponent<BuiltinInt>().bit = 8;
            else if (messageType == typeof(short))
                obj.AddComponent<BuiltinInt>().bit = 16;
            else if (messageType == typeof(int))
                obj.AddComponent<BuiltinInt>().bit = 32;
            else if (messageType == typeof(long))
                obj.AddComponent<BuiltinInt>().bit = 64;

            else if (messageType == typeof(byte))
                obj.AddComponent<BuiltinUInt>().bit = 8;
            else if (messageType == typeof(ushort))
                obj.AddComponent<BuiltinUInt>().bit = 16;
            else if (messageType == typeof(uint))
                obj.AddComponent<BuiltinUInt>().bit = 32;
            else if (messageType == typeof(ulong))
                obj.AddComponent<BuiltinUInt>().bit = 64;

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
