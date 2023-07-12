using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ROSTopic2GameObject;

public class SliderInput : MonoBehaviour
{
    private Slider slider;
    [SerializeField] private BuiltinFloat f;

    // Start is called before the first frame update
    void Start()
    {
        this.slider = this.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        this.f.val = this.slider.value;
    }
}
