using UnityEngine;

public class GrenadeLogic : MonoBehaviour
{
    private Rigidbody _rigidbody;

    public void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    public void Update()
    {
        Debug.DrawLine(transform.position, transform.position + transform.forward * 3, Color.red);
        Debug.DrawLine(transform.position, transform.position + Vector3.up * 3, Color.black);
        var rotation = Quaternion.RotateTowards(_rigidbody.rotation, Quaternion.identity, 1080 * Time.deltaTime);
        var angle = Vector3.SignedAngle(transform.forward, Vector3.up, transform.right);

        //_rigidbody.AddTorque(rotation * Vector3.right * 40, ForceMode.Acceleration);
        //_rigidbody.MoveRotation(_rigidbody.rotation * Quaternion.AngleAxis(angle * 0.1f, transform.right));
    }
}
