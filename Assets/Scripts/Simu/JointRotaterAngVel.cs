using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ROSTopic2GameObject;

public class JointRotaterAngVel : MonoBehaviour
{
    [SerializeField] private float coef;
    [SerializeField] private BuiltinFloat rpm;
    private ArticulationBody joint;

    // Start is called before the first frame update
    void Start()
    {
        this.joint = this.GetComponent<ArticulationBody>();
    }

    // Update is called once per frame
    void Update()
    {
        ArticulationDrive xDrive = this.joint.xDrive;
        xDrive.targetVelocity = this.coef * (float)this.rpm.val * 6f; //   6 = 360/60 = 2pi / 60sec
        this.joint.xDrive = xDrive;
    }
}
