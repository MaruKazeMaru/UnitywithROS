using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class VelGet : MonoBehaviour
{
    [SerializeField] private ROSfloat64 z;
    [SerializeField] private float zCoef;
    [SerializeField] private ROSfloat64 yaw;
    [SerializeField] private float yawCoef;
    private Rigidbody _rigidbody;

    private void Start()
    {
        this._rigidbody = this.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Vector3 linVel = Vector3.zero;
        if (this.z != null)
            linVel.z = this.z.val * this.zCoef;
        this._rigidbody.AddRelativeForce(linVel, ForceMode.VelocityChange);

        Vector3 angVel = Vector3.zero;
        if (this.yaw != null)
            angVel.y = this.yaw.val * this.yawCoef;
        this._rigidbody.AddRelativeTorque(angVel, ForceMode.VelocityChange);
    }
}
*/