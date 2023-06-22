using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using TwistMsg = RosMessageTypes.Geometry.TwistMsg;

public class ROSSubTest : MonoBehaviour
{
    [SerializeField] private ROSBuiltinFloat64 x;
    [SerializeField] private ROSBuiltinFloat64 y;
    [SerializeField] private ROSBuiltinFloat64 z;
    [SerializeField] private ROSBuiltinFloat64 pitch;
    [SerializeField] private ROSBuiltinFloat64 yaw;
    [SerializeField] private ROSBuiltinFloat64 roll;

    void Start()
    {
        var roscon = ROSConnection.GetOrCreateInstance();
        roscon.Subscribe<TwistMsg>("/cmd_vel", this.Callback);
    }

    void Callback(TwistMsg msg)
    {
        this.x.val = (float)msg.linear.x;
        this.y.val = (float)msg.linear.y;
        this.z.val = (float)msg.linear.z;

        this.pitch.val = (float)msg.angular.x;
        this.yaw.val   = (float)msg.angular.y;
        this.roll.val  = (float)msg.angular.z;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
