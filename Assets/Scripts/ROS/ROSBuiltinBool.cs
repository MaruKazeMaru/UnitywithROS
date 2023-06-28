using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ROSBuiltinBool : ROSField
{
    public bool val;

    public override void SetVal(object val)
    {
        this.val = (bool)val;
    }
}
