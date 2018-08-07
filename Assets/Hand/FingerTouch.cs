using UnityEngine;

public class FingerTouch : MonoBehaviour
{
    public bool IsOnFinger;
    private int _fingerCollidersTouching;

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
        IsOnFinger = _fingerCollidersTouching > 0;
    }
}