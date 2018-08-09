using UnityEngine;

public class FingerTouch : MonoBehaviour
{
    public bool IsOnFinger;
    private int _fingerCollidersTouching;
    private PinState _pinState;

    public void Start()
    {
        _pinState = GetComponentInParent<PinState>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 11)
        {
            _fingerCollidersTouching += 1;
            UpdateIsOnFinger();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 11)
        {
            _fingerCollidersTouching -= 1;
            UpdateIsOnFinger();
        }
    }

    private void UpdateIsOnFinger()
    {
        _pinState.IsOnFinger = _fingerCollidersTouching > 0;
    }
}