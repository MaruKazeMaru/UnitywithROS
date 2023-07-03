using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using Float32MultiArray = RosMessageTypes.Std.Float32MultiArrayMsg;
using Unity.Robotics.ROSTCPConnector;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace ROSUnity
{
    public class Topic : MonoBehaviour
    {
        protected Interface rosinterface;
        public string topicName;

        private void Start()
        {
            this.rosinterface = this.GetComponent<Interface>();
        }

        public virtual void Register(ROSConnection con) { }
    }
}
