using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;

public class ROSCon : MonoBehaviour
{
    [SerializeField] private List<ROSTopic> topics;
    private List<ROSTopic> registeredTopics;
    private ROSConnection roscon;

    // Start is called before the first frame update
    void Start()
    {
        this.roscon = ROSConnection.GetOrCreateInstance();

        this.registeredTopics = new List<ROSTopic>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            foreach (ROSTopic topic in this.topics)
            {
                if (!this.registeredTopics.Contains(topic))
                {
                    topic.RegisterPubSub(this.roscon);
                    this.registeredTopics.Add(topic);
                }
            }
        }
    }
}
