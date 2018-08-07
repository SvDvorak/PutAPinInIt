using UnityEngine;

public class FaceAnimator : MonoBehaviour
{
    public Animator EyeLeft;
    public Animator EyeRight;
    public Animator Mouth;
    public float BlinkTime;

    private float _blinkTimer;

    private int _pinsInProximity;

    public void Start()
    {
        _blinkTimer = BlinkTime;
    }

    public void Update()
    {
        if (_blinkTimer < Time.time)
        {
            EyeLeft.ResetTrigger("blink");
            EyeRight.ResetTrigger("blink");

            EyeLeft.SetTrigger("blink");
            EyeRight.SetTrigger("blink");
            _blinkTimer = Time.time + BlinkTime * (0.8f + Random.value);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        _pinsInProximity += 1;

        EyeLeft.SetBool("angry", true);
        EyeRight.SetBool("angry", true);
        Mouth.SetBool("angry", true);
    }

    public void OnTriggerExit(Collider other)
    {
        _pinsInProximity -= 1;

        if (_pinsInProximity <= 0)
        {
            EyeLeft.SetBool("angry", false);
            EyeRight.SetBool("angry", false);
            Mouth.SetBool("angry", false);
        }
    }
}
