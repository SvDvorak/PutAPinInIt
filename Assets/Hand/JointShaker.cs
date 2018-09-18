using UnityEngine;

public class JointShaker : MonoBehaviour
{
    private Vector3 _initialLocalPosition;
    private Quaternion _initialLocalRotation;

    public void Start()
    {
        _initialLocalPosition = transform.localPosition;
        _initialLocalRotation = transform.localRotation;
    }

    public void Update()
    {
        //transform.localPosition = _initialLocalPosition + Random.insideUnitSphere*0.01f;
        //transform.localRotation = Quaternion.Euler(Random.value, Random.value, Random.value) * _initialLocalRotation;
    }
}