using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROSTopic2GameObject
{
    public class Array : Field
    {
        [SerializeField, ReadOnly] public string elementTypeName;
        public Type elementType;
        private object val;
        private List<Field> elements;

        private void InitElementTransforms()
        {
            this.elements = new List<Field>();
            foreach (Transform child in this.transform)
                if (child.TryGetComponent<Field>(out Field field))
                    this.elements.Add(field);
        }

        private void Start()
        {
            this.elementType = MessageInfoGetter.GetType(this.elementTypeName);
            this.InitElementTransforms();
        }

        public override void SetVal(object val)
        {
            var arr = (System.Array)val;
            int m = arr.Length;
            int n = this.elements.Count;
            if (m > n)
            {
                var obj = new GameObject($"element ({n})");
                obj.transform.parent = this.transform;
                TopicObjCreater.Create(obj, this.elementType);
                this.elements.Add(obj.GetComponent<Field>());

                for(int i = n + 1; i < m; ++i)
                {
                    GameObject objCopy = Instantiate(obj, this.transform);
                    objCopy.name = $"element ({i})";
                    this.elements.Add(objCopy.GetComponent<Field>());
                }
            }

            for (int i = 0; i < m; ++i)
                this.elements[i].SetVal(arr.GetValue(i));
            this.val = val;
        }

        public override object GetVal()
        {
            var arr = System.Array.CreateInstance(this.elementType, this.elements.Count);
            int n = this.elements.Count;
            for(int i = 0; i < n; ++i)
                arr.SetValue(this.elements[i].GetVal(), i);
            this.val = arr;
            return this.val;
        }
    }
}
