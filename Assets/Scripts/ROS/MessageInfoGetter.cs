using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROSTopic2GameObject
{
    public static class MessageInfoGetter
    {
        private static Type[] msgs = null;
        private static Type[] Msgs
        {
            get
            {
                if (msgs == null)
                {
                    // メッセージが定義されている名前空間と名前空間同じクラスを全て取得
                    foreach (AssemblyName aname in Assembly.GetExecutingAssembly().GetReferencedAssemblies())
                    {
                        if (aname.Name == "Unity.Robotics.ROSTCPConnector.Messages")
                        {
                            msgs = Assembly.Load(aname).GetTypes();
                            break;
                        }
                    }
                }
                return msgs;
            }
        }

        public static Type GetType(string typeFullName)
        {
            foreach(Type type in Msgs)
                if(type.FullName == typeFullName)
                    return type;

            return Type.GetType(typeFullName);
        }

        public static Type GetType(string packageName, string messageName)
        {
            string messageFullName = packageName + "/" + messageName;

            // メッセージのメンバー変数求める
            foreach (Type t in Msgs)
            {
                //Debug.Log("t.Namespace=" + t.Namespace + " t.Name=" + t.Name);

                // ROS TCP-Connector が生成したメッセージクラスはいずれも
                // string k_RosMessageName = "<パッケージ名>/<メッセージ名>"
                // というstring型の変数が用意される
                FieldInfo info = t.GetField("k_RosMessageName");

                // この変数が存在するか確認
                if (info == null)
                    continue;

                // 中身が当クラスで指定したパッケージ、メッセージと合ってるか確認
                string k_rosMessageName = (string)info.GetValue(t);
                //Debug.Log(k_rosMessageName);
                if (k_rosMessageName == messageFullName)
                {
                    return t;
                }
            }

            return null;
        }

        public static FieldInfo[] GetFieldInfos(string packageName, string messageName)
        {
            return GetFieldInfos(GetType(packageName, messageName));
        }

        public static FieldInfo[] GetFieldInfos(Type messageType)
        {
            if (messageType == null) return null;

            var finfos = new List<FieldInfo>(messageType.GetFields());
            for (int i = finfos.Count - 1; i >= 0; --i)
            {
                if (finfos[i].Name == "k_RosMessageName")
                {
                    finfos.RemoveAt(i);
                    break;
                }
            }
            return finfos.ToArray();
        }
    }
}