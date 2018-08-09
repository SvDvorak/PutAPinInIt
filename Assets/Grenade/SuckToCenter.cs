using UnityEngine;

public class Stage2Suck : MonoBehaviour
{
    private GrenadeLogic _grenadeLogic;

    public void Start()
    {
        _grenadeLogic = GetComponentInParent<GrenadeLogic>();
    }

    public void OnTriggerEnter(Collider other)
    {
        _grenadeLogic.PinOnTheWayInShaft = other.attachedRigidbody;
    }

    public void OnTriggerExit(Collider other)
    {
        _grenadeLogic.PinOnTheWayInShaft = null;
    }
}
