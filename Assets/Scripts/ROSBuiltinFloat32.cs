using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ROSBuiltinFloat32 : ROSField
{
    public float val;

    public override void SetVal(object val)
    {
        this.val = (float)val;
    }
}
