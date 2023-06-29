using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowInput : MonoBehaviour
{
    [SerializeField] private ROSBuiltinFloat64 lrTopic;
    [SerializeField] private ROSBuiltinFloat64 udTopic;

    [SerializeField] private KeyCode lKey;
    [SerializeField] private KeyCode rKey;
    [SerializeField] private KeyCode uKey;
    [SerializeField] private KeyCode dKey;

    private CanvasGroup lCnvsGrp;
    private CanvasGroup rCnvsGrp;
    private CanvasGroup uCnvsGrp;
    private CanvasGroup dCnvsGrp;

    private float keyUpAlpha = 0.3f;
    private float keyDownAlpha = 1f;

    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform child in this.transform)
        {
            switch (child.name)
            {
                case "l":
                    this.lCnvsGrp = child.GetComponent<CanvasGroup>();
                    this.lCnvsGrp.alpha = this.keyUpAlpha;
                    break;
                case "r":
                    this.rCnvsGrp = child.GetComponent<CanvasGroup>();
                    this.rCnvsGrp.alpha = this.keyUpAlpha;
                    break;
                case "u":
                    this.uCnvsGrp = child.GetComponent<CanvasGroup>();
                    this.uCnvsGrp.alpha = this.keyUpAlpha;
                    break;
                case "d":
                    this.dCnvsGrp = child.GetComponent<CanvasGroup>();
                    this.dCnvsGrp.alpha = this.keyUpAlpha;
                    break;
                default:
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        int lr = 0;
        if (Input.GetKey(this.lKey))
        {
            this.lCnvsGrp.alpha = this.keyDownAlpha;
            lr -= 1;
        }
        else
            this.lCnvsGrp.alpha = this.keyUpAlpha;
        if (Input.GetKey(this.rKey))
        {
            this.rCnvsGrp.alpha = this.keyDownAlpha;
            lr += 1;
        }
        else
            this.rCnvsGrp.alpha = this.keyUpAlpha;

        int ud = 0;
        if (Input.GetKey(this.uKey))
        {
            this.uCnvsGrp.alpha = this.keyDownAlpha;
            ud += 1;
        }
        else
            this.uCnvsGrp.alpha = this.keyUpAlpha;
        if (Input.GetKey(this.dKey))
        {
            this.dCnvsGrp.alpha = this.keyDownAlpha;
            ud -= 1;
        }
        else
            this.dCnvsGrp.alpha = this.keyUpAlpha;

        this.lrTopic.SetVal((double)lr);
        this.udTopic.SetVal((double)ud);
    }
}
