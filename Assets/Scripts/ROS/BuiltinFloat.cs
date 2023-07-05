using UnityEngine;

namespace ROSUnity
{
    public class BuiltinFloat : Field
    {
        [SerializeField, ReadOnly] public byte bit;
        public double val;

        public override void SetVal(object val) { this.val = (double)val; }

        public override object GetVal()
        {
            if (this.bit == 32)
                return (float)this.val;
            else
                return this.val;
        }
    }
}
