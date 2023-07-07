using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace ROSTopic2GameObject
{
    public class Publisher : Topic
    {
        public float frequency;
        public int queueSize;

        private ROSConnection roscon = null;
        private float timeFromLastPublish = 0f;

        public override void Register(ROSConnection con)
        {
            this.roscon = con;
            if (this.queueSize <= 0)
                this.queueSize = 1;
            this.roscon.RegisterPublisher(this.topicName, this.rosinterface.MessageFullName, this.queueSize);
            Debug.Log("registered " + this.topicName);
        }

        private void Update()
        {
            if (this.roscon != null)
            {
                if (this.frequency <= 0f)
                    this.frequency = 1f;
                float interval = 1f / this.frequency;

                this.timeFromLastPublish += Time.deltaTime;

                if (this.timeFromLastPublish >= interval)
                {
                    this.roscon.Publish(this.topicName, (Message)this.rosinterface.GetVal());
                    //Debug.Log("published");
                    this.timeFromLastPublish = 0f;
                }
            }
        }
    }
}
