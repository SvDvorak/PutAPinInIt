using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupPin : MonoBehaviour
{
    public Transform FingerPinHangPoint;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            var isLockedInGrenade = other.gameObject.GetComponentInParent<PinState>().IsLockedInGrenade;
            if(!isLockedInGrenade)
            {
                other.attachedRigidbody.MoveRotation(transform.rotation);
                other.attachedRigidbody.MovePosition(FingerPinHangPoint.position);
            }
        }
    }
}
