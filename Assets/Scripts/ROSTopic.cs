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
    protected ROSInterface rosinterface;
    public string topicName;

    private void Start()
    {
        this.rosinterface = this.GetComponent<ROSInterface>();
    }

    public virtual void Register(ROSConnection con) { }
}
