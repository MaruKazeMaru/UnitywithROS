using UnityEngine;

namespace ROSUnity
{
    public class Field : MonoBehaviour
    {
        public virtual void SetVal(object val) { }
        public virtual object GetVal() { return null; }
    }
}