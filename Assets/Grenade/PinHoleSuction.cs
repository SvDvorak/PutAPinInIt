using UnityEngine;

public class PinHoleSuction : MonoBehaviour
{
    public Transform HoleCenter;
    public float Force;

    public void Start()
    {

    }

    public void Update()
    {

    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            var holeCenter = HoleCenter.position;
            var point = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(holeCenter);
            var diff = point - holeCenter; 
            Debug.DrawLine(holeCenter, holeCenter + diff * 10, Color.black);
            //Debug.Break();
            other.attachedRigidbody.AddForce((holeCenter - point).normalized*Force);
        }
    }
}
