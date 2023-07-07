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
                    // ���b�Z�[�W����`����Ă��閼�O��ԂƖ��O��ԓ����N���X��S�Ď擾
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

            // ���b�Z�[�W�̃����o�[�ϐ����߂�
            foreach (Type t in Msgs)
            {
                //Debug.Log("t.Namespace=" + t.Namespace + " t.Name=" + t.Name);

                // ROS TCP-Connector �������������b�Z�[�W�N���X�͂������
                // string k_RosMessageName = "<�p�b�P�[�W��>/<���b�Z�[�W��>"
                // �Ƃ���string�^�̕ϐ����p�ӂ����
                FieldInfo info = t.GetField("k_RosMessageName");

                // ���̕ϐ������݂��邩�m�F
                if (info == null)
                    continue;

                // ���g�����N���X�Ŏw�肵���p�b�P�[�W�A���b�Z�[�W�ƍ����Ă邩�m�F
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