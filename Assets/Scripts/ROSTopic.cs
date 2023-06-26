using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using Float32MultiArray = RosMessageTypes.Std.Float32MultiArrayMsg;
using Unity.Robotics.ROSTCPConnector;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

public class ROSTopic : MonoBehaviour
{
    private ROSInterface rosinterface;
    public string topicName;
    private enum PubSub { Pub, Sub }
    [SerializeField] private PubSub pubsub;

    private void Start()
    {
        this.rosinterface = this.GetComponent<ROSInterface>();
    }

    public void RegisterPubSub(ROSConnection con)
    {
        switch (this.pubsub)
        {
            case PubSub.Pub:
                con.RegisterPublisher(this.topicName, this.rosinterface.MessageFullName, 1);
                break;
            case PubSub.Sub:
                con.SubscribeByMessageName(this.topicName, this.rosinterface.MessageFullName, this.Callback);
                break;
            default:
                break;
        }
    }

    private void Callback(Message msg)
    {
        this.rosinterface.SetVal(msg);
    }

    /*
    
    [SerializeField] private string packageName;
    [SerializeField] private string typeName;
    private void Start()
    {
        string pkg = "RosMessageTypes." + packageName;
        string typ = typeName;
//        var exeAssembly = Assembly.GetExecutingAssembly();
//        foreach (AssemblyName assemblyName in exeAssembly.GetReferencedAssemblies())
//        {
//            Debug.Log(assemblyName);
//        }
        //        Assembly assembly;

        Type[] msgs = new Type[] { };
        foreach(AssemblyName aname in Assembly.GetExecutingAssembly().GetReferencedAssemblies())
        {
            if(aname.Name == "Unity.Robotics.ROSTCPConnector.Messages")
            {
                msgs = Assembly.Load(aname).GetTypes();
                break;
            }
        }

        Type type = Type.GetType("float");
        foreach(Type t in msgs)
        {
            if(t.Namespace == pkg && t.Name == typ)
            {
                type = t;
                Debug.Log("found " + pkg + "." + typ);
                break;
            }
        }

        foreach (FieldInfo info in type.GetFields()) 
        {
            Debug.Log(info.Name);
        }
    }
*/
}
