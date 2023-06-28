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

    // key : 下層の変数名(ex, Vector3 ならば x,y,z)
    // val : 〃　　　　に対応するROSTopicインスタンス
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

        // key:メッセージの変数名
        // val:変数(のインスタンス)格納用のROSFieldインスタンス
        // のディクショナリ初期化
        this.fields = new Dictionary<string, ROSField>();

        foreach (FieldInfo fieldInfo in this.fieldInfos)
        {
            //Debug.Log(fieldInfo.Name);
            //Debug.Log(fieldInfo.FieldType);

            Transform fieldTransform = this.transform; //右辺は未割当てエラーが出ないようにするためのもの。なんでもOK
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
                //ここに対応する子オブジェクトが無かった場合の処理
                // 1 オブジェクト生成
                // 2 ROSFIeld or その子スクリプトをアタッチ
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
