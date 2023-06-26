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

    // key : 下層の変数名(ex, Vector3 ならば x,y,z)
    // val : 〃　　　　に対応するROSTopicインスタンス
    protected Dictionary<string, ROSField> fields;
    protected Type messageType;
    protected FieldInfo[] fieldInfos;

    // Start is called before the first frame update
    protected void Start()
    {
        // メッセージが定義されている名前空間と名前空間同じクラスを全て取得
        Type[] msgs = new Type[] { };
        foreach (AssemblyName aname in Assembly.GetExecutingAssembly().GetReferencedAssemblies())
        {
            if (aname.Name == "Unity.Robotics.ROSTCPConnector.Messages")
            {
                msgs = Assembly.Load(aname).GetTypes();
                break;
            }
        }

        // メッセージのメンバー変数求める
        var finfos = new List<FieldInfo>();
        foreach (Type t in msgs)
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
}
