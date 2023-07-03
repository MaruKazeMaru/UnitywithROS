using UnityEngine;

namespace ROSUnity
{
    public class BuiltinFloat : Field
    {
        [SerializeField] public double val;
        public double Val => this.val;
        public float Valf => (float)this.val;

        public override void SetVal(object val) { this.val = (double)val; }

        public override object GetVal()
        {
            //Debug.Log("get f");
            return this.val;
        }
    }
}
