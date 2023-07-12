using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ROSTopic2GameObject;

public class JointRotaterAng : MonoBehaviour
{
    [SerializeField] private float coef;
    [SerializeField] private BuiltinFloat deg;
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
        xDrive.target = this.coef * (float)this.deg.val;
        this.joint.xDrive = xDrive;
    }
}
