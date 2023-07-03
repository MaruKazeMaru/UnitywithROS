using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ROSUnity;

public class VelGet : MonoBehaviour
{
    [SerializeField] private BuiltinFloat go;
    [SerializeField] private float goCoef;
    [SerializeField] private BuiltinFloat turn;
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
            float z = this.goCoef * this.go.Valf;
            this._rigidbody.AddRelativeForce(new Vector3(0f, 0f, z), ForceMode.VelocityChange);
        }
        if (this.turn != null)
        {
            float p = this.tuenCoef * this.turn.Valf;
            this._rigidbody.AddRelativeTorque(new Vector3(0f, p, 0f), ForceMode.VelocityChange);
        }
    }
}
