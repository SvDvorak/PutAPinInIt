using UnityEngine;

public class GrenadePinTrigger : MonoBehaviour
{
    private GrenadeLogic _grenadeLogic;

    public void Start()
    {
        _grenadeLogic = GetComponentInParent<GrenadeLogic>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            _grenadeLogic.ActivePin = other.attachedRigidbody;
            other.gameObject.GetComponentInParent<PinState>().IsLockedInGrenade = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            _grenadeLogic.ActivePin = null;
            other.gameObject.GetComponentInParent<PinState>().IsLockedInGrenade = false;
        }
    }
}
