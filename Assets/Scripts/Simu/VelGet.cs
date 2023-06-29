using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelGet : MonoBehaviour
{
    [SerializeField] private ROSBuiltinFloat64 go;
    [SerializeField] private float goCoef;
    [SerializeField] private ROSBuiltinFloat64 turn;
    [SerializeField] private float tuenCoef;

    private Rigidbody _rigidbody;

    private void Start()
    {
        this._rigidbody = this.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (go != null)
        {
            float z = this.goCoef * (float)this.go.val;
            this._rigidbody.AddRelativeForce(new Vector3(0f, 0f, z), ForceMode.VelocityChange);
        }
        if (this.turn != null)
        {
            float p = this.tuenCoef * (float)this.turn.val;
            this._rigidbody.AddRelativeTorque(new Vector3(0f, p, 0f), ForceMode.VelocityChange);
        }
    }
}
