using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using Float32Msg = RosMessageTypes.Std.Float32Msg;

public class ROSPubTest : MonoBehaviour
{
    private ROSConnection roscon;
    private string go  = "/go";
    private string turn  = "/turn";

    [SerializeField] private CanvasGroup u;
    [SerializeField] private CanvasGroup d;
    [SerializeField] private CanvasGroup l;
    [SerializeField] private CanvasGroup r;
    [SerializeField] private float disactiveAlpha;
    [SerializeField] private float activeAlpha;

    void Start()
    {
        this.roscon = ROSConnection.GetOrCreateInstance();
        this.roscon.RegisterPublisher<Float32Msg>(this.go);
        this.roscon.RegisterPublisher<Float32Msg>(this.turn);

        this.u.alpha = this.disactiveAlpha;
        this.d.alpha = this.disactiveAlpha;
        this.l.alpha = this.disactiveAlpha;
        this.r.alpha = this.disactiveAlpha;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(Input.GetKey(KeyCode.DownArrow))
                this.roscon.Publish(this.go, new Float32Msg(0f));
            else
                this.roscon.Publish(this.go, new Float32Msg(1f));
            u.alpha = activeAlpha;
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            if (Input.GetKey(KeyCode.DownArrow))
                this.roscon.Publish(this.go, new Float32Msg(-1f));
            else
                this.roscon.Publish(this.go, new Float32Msg(0f));
            u.alpha = disactiveAlpha;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (Input.GetKey(KeyCode.UpArrow))
                this.roscon.Publish(this.go, new Float32Msg(0f));
            else
                this.roscon.Publish(this.go, new Float32Msg(-1f));
            d.alpha = activeAlpha;
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            if (Input.GetKey(KeyCode.UpArrow))
                this.roscon.Publish(this.go, new Float32Msg(1f));
            else
                this.roscon.Publish(this.go, new Float32Msg(0f));
            d.alpha = disactiveAlpha;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (Input.GetKey(KeyCode.RightArrow))
                this.roscon.Publish(this.turn, new Float32Msg(0f));
            else
                this.roscon.Publish(this.turn, new Float32Msg(-1f));
            l.alpha = activeAlpha;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            if (Input.GetKey(KeyCode.RightArrow))
                this.roscon.Publish(this.turn, new Float32Msg(1f));
            else
                this.roscon.Publish(this.turn, new Float32Msg(0f));
            l.alpha = disactiveAlpha;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (Input.GetKey(KeyCode.LeftArrow))
                this.roscon.Publish(this.turn, new Float32Msg(0f));
            else
                this.roscon.Publish(this.turn, new Float32Msg(1f));
            r.alpha = activeAlpha;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            if (Input.GetKey(KeyCode.LeftArrow))
                this.roscon.Publish(this.turn, new Float32Msg(-1f));
            else
                this.roscon.Publish(this.turn, new Float32Msg(0f));
            r.alpha = disactiveAlpha;
        }
    }
}
