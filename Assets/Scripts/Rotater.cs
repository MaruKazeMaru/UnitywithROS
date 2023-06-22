using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotater : MonoBehaviour
{
    [SerializeField] private ROSBuiltinFloat64 angVel;
    [SerializeField] float angVelCoef;

    private void Start()
    {
    }

    private void Update()
    {
        this.transform.Rotate(0f, (float)this.angVel.val, 0f);
    }
}
