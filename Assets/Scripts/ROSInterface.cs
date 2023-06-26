using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;
using ROSUnity;

public class ROSInterface : ROSField
{
    [SerializeField, ReadOnly] protected string packageName;
    [SerializeField, ReadOnly] protected string messageName;
    public void SetMessageName(string packageName, string messageName)
    {
        this.packageName = packageName;
        this.messageName = messageName;
    }
    public void SetMessageName(string messageFullName)
    {
        string[] s = messageFullName.Split('/');
        this.packageName = s[0];
        this.messageName = s[1];
    }
    public string MessageFullName => this.packageName + "/" + this.messageName;

    protected Message val;

    // key : ���w�̕ϐ���(ex, Vector3 �Ȃ�� x,y,z)
    // val : �V�@�@�@�@�ɑΉ�����ROSTopic�C���X�^���X
    protected Dictionary<string, ROSField> fields;
    protected Type messageType;
    protected FieldInfo[] fieldInfos;

    // Start is called before the first frame update
    protected void Start()
    {
        // ���b�Z�[�W����`����Ă��閼�O��ԂƖ��O��ԓ����N���X��S�Ď擾
        Type[] msgs = new Type[] { };
        foreach (AssemblyName aname in Assembly.GetExecutingAssembly().GetReferencedAssemblies())
        {
            if (aname.Name == "Unity.Robotics.ROSTCPConnector.Messages")
            {
                msgs = Assembly.Load(aname).GetTypes();
                break;
            }
        }

        // ���b�Z�[�W�̃����o�[�ϐ����߂�
        var finfos = new List<FieldInfo>();
        foreach (Type t in msgs)
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
            if (k_rosMessageName == this.MessageFullName)
            {
                this.messageType = t;
                finfos = new List<FieldInfo>(t.GetFields());
                break;
            }
        }
        for (int i = finfos.Count - 1; i >= 0; --i)
        {
            if (finfos[i].Name == "k_RosMessageName")
            {
                finfos.RemoveAt(i);
                break;
            }
        }
        this.fieldInfos = finfos.ToArray();

        // key:���b�Z�[�W�̕ϐ���
        // val:�ϐ�(�̃C���X�^���X)�i�[�p��ROSField�C���X�^���X
        // �̃f�B�N�V���i��������
        this.fields = new Dictionary<string, ROSField>();

        foreach (FieldInfo fieldInfo in this.fieldInfos)
        {
            //Debug.Log(fieldInfo.Name);
            //Debug.Log(fieldInfo.FieldType);

            Transform fieldTransform = this.transform; //�E�ӂ͖������ăG���[���o�Ȃ��悤�ɂ��邽�߂̂��́B�Ȃ�ł�OK
            bool exist = false;
            foreach(Transform child in this.transform)
            {
                if(fieldInfo.Name == child.name)
                {
                    fieldTransform = child;
                    exist = true;
                    break;
                }
            }

            if (!exist)
            {
                //�����ɑΉ�����q�I�u�W�F�N�g�����������ꍇ�̏���
                // 1 �I�u�W�F�N�g����
                // 2 ROSFIeld or ���̎q�X�N���v�g���A�^�b�`
            }

            this.fields.Add(fieldInfo.Name, fieldTransform.GetComponent<ROSField>());
        }
    }

    public override void SetVal(object val)
    {
        this.val = (Message)val;

        foreach(FieldInfo fieldInfo in this.fieldInfos)
            this.fields[fieldInfo.Name].SetVal(fieldInfo.GetValue(val));
    }
}
