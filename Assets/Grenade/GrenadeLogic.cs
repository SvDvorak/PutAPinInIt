using UnityEngine;

public class GrenadeLogic : MonoBehaviour
{
    public bool IsPinTriggered;
    public Transform PinHoleCenter;
    public GameObject Face;
    public GameObject PinTemplate;

    private Rigidbody _rigidbody;

    public void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        if (IsPinTriggered)
        {
            Instantiate(PinTemplate, PinHoleCenter.position + PinHoleCenter.rotation * new Vector3(0, 1, 0), PinHoleCenter.rotation * Quaternion.Euler(0, 0, -90));
        }
    }

    public void Update()
    {
        Face.SetActive(!IsPinTriggered);

        Debug.DrawLine(transform.position, transform.position + transform.forward * 3, Color.red);
        Debug.DrawLine(transform.position, transform.position + Vector3.up * 3, Color.black);
        var rotation = Quaternion.RotateTowards(_rigidbody.rotation, Quaternion.identity, 1080 * Time.deltaTime);
        var angle = Vector3.SignedAngle(transform.forward, Vector3.up, transform.right);

        //_rigidbody.AddTorque(rotation * Vector3.right * 40, ForceMode.Acceleration);
        //_rigidbody.MoveRotation(_rigidbody.rotation * Quaternion.AngleAxis(angle * 0.1f, transform.right));
    }
}
