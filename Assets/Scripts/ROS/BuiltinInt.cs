using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROSUnity
{
    public class BuiltinInt : Field
    {
        [SerializeField] public long val;

        public override void SetVal(object val) { this.val = (long)val; }

        public override object GetVal()
        {
            //Debug.Log("get f");
            return this.val;
        }
    }
}
