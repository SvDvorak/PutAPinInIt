using UnityEngine;

public class PinState : MonoBehaviour
{
    public bool IsLockedInGrenade;
    public bool IsOnFinger { get { return _fingerTouch.IsOnFinger; } }
    private FingerTouch _fingerTouch;

    public void Start()
    {
        _fingerTouch = GetComponentInChildren<FingerTouch>();
    }
}