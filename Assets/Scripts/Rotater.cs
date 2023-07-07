using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ROSTopic2GameObject;

public class Rotater : MonoBehaviour
{
    [SerializeField] private BuiltinFloat angVel;
    [SerializeField] float angVelCoef;

    private void Start()
    {
    }

    private void Update()
    {
        this.transform.Rotate(0f, (float)this.angVel.val, 0f);
    }
}
