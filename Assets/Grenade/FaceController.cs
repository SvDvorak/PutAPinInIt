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
    private SoundShotPlayer _soundShotPlayer;
    private bool _isAngry;

    public void Start()
    {
        _blinkTimer = BlinkTime;
        _soundShotPlayer = GetComponentInParent<SoundShotPlayer>();
    }

    public void Update()
    {
        _isAngry = false;
        foreach (var pin in _pinsInProximity)
        {
            if (pin.IsOnFinger)
                _isAngry = true;
        }

        EyeLeft.SetBool("angry", _isAngry);
        EyeRight.SetBool("angry", _isAngry);
        Mouth.SetBool("angry", _isAngry);

        _doingVoice = _soundShotPlayer.IsPlayingVoice;

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

    [ContextMenu("Talk")]
    public void Talk()
    {
        _soundShotPlayer.PlayVoice(_isAngry ? "AngryChat" : "IdleChat");
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
