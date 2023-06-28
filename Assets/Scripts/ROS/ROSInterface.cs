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
    protected string messageFullName;

    public void Init(Type messageType)
    {
        FieldInfo k_name = messageType.GetField("k_RosMessageName");
        string s = (string)k_name.GetValue(null);
        string[] ss = s.Split('/');
        this.packageName = ss[0];
        this.messageName = ss[1];
    }

    public string MessageFullName => this.messageFullName;

    protected Message val;
    public Message Val => this.val;

    // key : ���w�̕ϐ���(ex, Vector3 �Ȃ�� x,y,z)
    // val : �V�@�@�@�@�ɑΉ�����ROSTopic�C���X�^���X
    protected Dictionary<string, ROSField> fields;
    protected Type messageType;
    protected FieldInfo[] fieldInfos;
    protected ConstructorInfo constructorInfo;

    // Start is called before the first frame update
    protected void Start()
    {
        this.messageFullName = this.packageName + "/" + this.messageName;
        this.messageType = MessageInfoGetter.GetType(this.packageName, this.messageName);
        this.fieldInfos = MessageInfoGetter.GetFieldInfos(this.messageType);
        this.constructorInfo = this.messageType.GetConstructor(Type.EmptyTypes);

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

    public override object GetVal()
    {
        this.val = (Message)this.constructorInfo.Invoke(null);
        foreach(FieldInfo fieldInfo in this.fieldInfos)
            fieldInfo.SetValue(this.val, this.fields[fieldInfo.Name].GetVal());
        //Debug.Log(this.val.ToString());
        return this.val;
    }
}
