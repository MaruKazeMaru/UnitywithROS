using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace ROSTopic2GameObject
{
    public class Subscriber : Topic
    {
        public override void Register(ROSConnection con)
        {
            con.SubscribeByMessageName(this.topicName, this.rosinterface.MessageFullName, this.Callback);
            Debug.Log("registered " + this.topicName);
        }

        private void Callback(Message msg)
        {
            this.rosinterface.SetVal(msg);
        }
    }
}
