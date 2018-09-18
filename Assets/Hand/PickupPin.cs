using UnityEngine;

public class PickupPin : MonoBehaviour
{
    public Transform FingerPinHangPoint;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            var pinState = other.gameObject.GetComponentInParent<PinState>();
            if(pinState == null || !pinState.IsLockedInGrenade && !pinState.IsOnFinger)
            {
                var ring = other.attachedRigidbody;
                var previousRingPosition = ring.position;

                var newRotation = Quaternion.Euler(0, 90, 90)*FingerPinHangPoint.rotation;
                var newPosition = FingerPinHangPoint.position;

                ring.isKinematic = true;

                foreach (var joint in ring.GetComponents<HingeJoint>())
                {
                    var connectedBody = joint.connectedBody;
                    connectedBody.isKinematic = true;
                    var diff = Quaternion.Inverse(ring.rotation) * (connectedBody.position - previousRingPosition);
                    connectedBody.transform.position = newPosition + newRotation * diff;
                    connectedBody.transform.rotation = newRotation;
                    connectedBody.isKinematic = false;
                }

                ring.transform.position = newPosition;
                ring.transform.rotation = newRotation;

                ring.isKinematic = false;
            }
        }
    }
}
