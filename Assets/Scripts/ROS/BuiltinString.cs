using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROSUnity
{
    public class BuiltinString : Field
    {
        [SerializeField] public string val;

        public override void SetVal(object val) { this.val = (string)val; }

        public override object GetVal()
        {
            //Debug.Log("get f");
            return this.val;
        }
    }
}
