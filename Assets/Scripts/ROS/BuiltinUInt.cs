using UnityEngine;

namespace ROSTopic2GameObject
{
    public class BuiltinUInt : Field
    {
        [SerializeField, ReadOnly] public byte bit;
        public ulong val;

        public override void SetVal(object val)
        {
            if (this.bit == 8)
                this.val = (byte)val;
            else if (this.bit == 16)
                this.val = (ushort)val;
            else if (this.bit == 32)
                this.val = (uint)val;
            else
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
