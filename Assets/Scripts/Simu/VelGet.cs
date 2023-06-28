using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelGet : MonoBehaviour
{
    [SerializeField] private ROSBuiltinFloat64 go;
    [SerializeField] private ROSBuiltinFloat64 turn;

    private Rigidbody _rigidbody;

    private void Start()
    {
        this._rigidbody = this.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float z = 0f;
        if (go != null)
            z = (float)this.go.val;
        float p = 0f;
        if (this.turn != null)
            p = (float)this.turn.val;

        this._rigidbody.AddRelativeForce( new Vector3(0f, 0f,  z), ForceMode.VelocityChange);
        this._rigidbody.AddRelativeTorque(new Vector3(0f,  p, 0f), ForceMode.VelocityChange);
    }
}
