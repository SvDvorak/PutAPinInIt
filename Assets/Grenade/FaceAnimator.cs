using System.Collections.Generic;
using UnityEngine;

public class FaceAnimator : MonoBehaviour
{
    public Animator EyeLeft;
    public Animator EyeRight;
    public Animator Mouth;
    public float BlinkTime;

    private float _blinkTimer;

    private List<PinState> _pinsInProximity = new List<PinState>();

    public void Start()
    {
        _blinkTimer = BlinkTime;
    }

    public void Update()
    {
        var isAnyPinOnFinger = false;
        foreach (var pin in _pinsInProximity)
        {
            if (pin.IsOnFinger)
                isAnyPinOnFinger = true;
        }

        EyeLeft.SetBool("angry", isAnyPinOnFinger);
        EyeRight.SetBool("angry", isAnyPinOnFinger);
        Mouth.SetBool("angry", isAnyPinOnFinger);

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
        _pinsInProximity.Add(other.GetComponentInParent<PinState>());
    }

    public void OnTriggerExit(Collider other)
    {
        _pinsInProximity.Remove(other.GetComponentInParent<PinState>());
    }
}
