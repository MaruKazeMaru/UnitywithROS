using UnityEngine;

public class ROSBuiltinFloat64 : ROSField
{
    public double val;

    public override void SetVal(object val) { this.val = (double)val; }

    public override object GetVal()
    {
        //Debug.Log("get f");
        return this.val;
    }
}