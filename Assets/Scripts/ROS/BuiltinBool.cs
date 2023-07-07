using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROSTopic2GameObject
{
    public class BuiltinBool : Field
    {
        [SerializeField] public bool val;

        public override void SetVal(object val) { this.val = (bool)val; }

        public override object GetVal()
        {
            //Debug.Log("get f");
            return this.val;
        }
    }
}
