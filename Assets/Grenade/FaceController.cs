using System.Collections.Generic;
using UnityEngine;

public class FaceController : MonoBehaviour
{
    public Animator EyeLeft;
    public Animator EyeRight;
    public Animator Mouth;
    public float BlinkTime;

    private float _blinkTimer;

    private readonly List<PinState> _pinsInProximity = new List<PinState>();
    private bool _doingVoice;

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

        EyeLeft.SetBool("talking", _doingVoice);
        EyeRight.SetBool("talking", _doingVoice);
        Mouth.SetBool("talking", _doingVoice);

        if (_blinkTimer < Time.time)
        {
            EyeLeft.ResetTrigger("blink");
            EyeRight.ResetTrigger("blink");

            EyeLeft.SetTrigger("blink");
            EyeRight.SetTrigger("blink");
            _blinkTimer = Time.time + BlinkTime * (0.8f + Random.value);
        }
    }

    [ExposeMethodInEditor]
    public void Talk()
    {
        _doingVoice = true;
    }

    public void AngryShout()
    {
        _doingVoice = true;
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
