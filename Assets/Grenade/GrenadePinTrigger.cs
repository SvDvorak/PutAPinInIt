using UnityEngine;

public class GrenadePinTrigger : MonoBehaviour
{
    public bool PinTriggered;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            PinTriggered = true;
            other.gameObject.GetComponentInParent<PinState>().IsLockedInGrenade = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            PinTriggered = false;
            other.gameObject.GetComponentInParent<PinState>().IsLockedInGrenade = false;
        }
    }
}
