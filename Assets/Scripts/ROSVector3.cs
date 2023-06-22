using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using Vector3Msg = RosMessageTypes.Geometry.Vector3Msg;

public class ROSVector3 : ROSInterface
{
    [SerializeField] double x;
    [SerializeField] double y;
    [SerializeField] double z;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            this.SetVal(new Vector3Msg(x, y, z));
            Debug.Log($"set x={this.x} y={this.y} z={this.z}");
        }
    }

    /*
    public Vector3Msg val;
    private ROSfloat64 x;
    private ROSfloat64 y;
    private ROSfloat64 z;

    private void SetVal(Vector3Msg v)
    {
        this.val = v;
        this.x.val = (float)v.x;
        this.y.val = (float)v.y;
        this.z.val = (float)v.z;
    }

    protected override void StartPub()
    {
        this.roscon.RegisterPublisher<Vector3Msg>(this.topicName, 2);
    }

    protected override void StartSub()
    {
        this.roscon.Subscribe<Vector3Msg>(this.topicName, this.Callback);
    }

    private void Callback(Vector3Msg msg)
    {
        this.SetVal(msg);
    }
    */
}
