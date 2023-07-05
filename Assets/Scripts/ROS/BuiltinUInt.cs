using UnityEngine;

namespace ROSUnity
{
    public class BuiltinUInt : Field
    {
        [SerializeField, ReadOnly] public byte bit;
        public ulong val;

        public override void SetVal(object val)
        {
            this.val = (ulong)val;
        }

        public override object GetVal()
        {
            if (this.bit == 8)
                return (byte)this.val;
            else if (this.bit == 16)
                return (ushort)this.val;
            else if (this.bit == 32)
                return (uint)this.val;
            else
                return this.val;
        }
    }
}
