using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;

namespace ROSTopic2GameObject
{
    public class TopicManager : MonoBehaviour
    {
        [SerializeField] private List<Topic> topics;
        private List<Topic> registeredTopics;
        private ROSConnection roscon;

        // Start is called before the first frame update
        void Start()
        {
            this.roscon = ROSConnection.GetOrCreateInstance();

            this.registeredTopics = new List<Topic>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                foreach (Topic topic in this.topics)
                {
                    if (!this.registeredTopics.Contains(topic))
                    {
                        topic.Register(this.roscon);
                        this.registeredTopics.Add(topic);
                    }
                }
            }
        }
    }
}
