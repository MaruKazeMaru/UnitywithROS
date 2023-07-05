using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROSUnity
{
    public class BuiltinInt : Field
    {
        [SerializeField, ReadOnly] public byte bit;
        public long val;

        public override void SetVal(object val)
        {
            if (this.bit == 8)
                this.val = (sbyte)val;
            else if (this.bit == 16)
                this.val = (short)val;
            else if (this.bit == 32)
                this.val = (int)val;
            else
                this.val = (long)val;
        }

        public override object GetVal()
        {
            if (this.bit == 8)
                return (sbyte)this.val;
            else if (this.bit == 16)
                return (short)this.val;
            else if (this.bit == 32)
                return (int)this.val;
            else
                return this.val;
        }
    }
}
