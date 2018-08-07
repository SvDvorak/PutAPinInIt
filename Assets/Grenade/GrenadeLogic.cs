using System.Collections.Generic;
using UnityEngine;

public class GrenadeLogic : MonoBehaviour
{
    public bool StartWithPin;
    public Transform PinHoleCenter;
    public GameObject Face;
    public GameObject PinTemplate;
    public List<GameObject> SuctionFields;

    private Rigidbody _rigidbody;
    public Rigidbody ActivePin;

    private bool _pushOutPin;

    public void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        if (StartWithPin)
        {
            Instantiate(PinTemplate, PinHoleCenter.position + PinHoleCenter.rotation * new Vector3(0, 1, 0), PinHoleCenter.rotation * Quaternion.Euler(0, 0, -90));
        }
    }

    public void Update()
    {
        Face.SetActive(ActivePin == null);

        Debug.DrawLine(transform.position, transform.position + transform.up * 3, Color.red);
        //Debug.DrawLine(transform.position, transform.position + Vector3.up * 3, Color.black);
        var rotation = Quaternion.RotateTowards(_rigidbody.rotation, Quaternion.identity, 1080 * Time.deltaTime);
        var angle = Vector3.SignedAngle(transform.forward, Vector3.up, transform.right);

        //_rigidbody.AddTorque(rotation * Vector3.right * 40, ForceMode.Acceleration);
        //_rigidbody.MoveRotation(_rigidbody.rotation * Quaternion.AngleAxis(angle * 0.1f, transform.right));

        if (_pushOutPin && ActivePin != null)
        {
            ActivePin.AddForce(ActivePin.rotation * -Vector3.up * 10, ForceMode.Force);
            WilDebug.DrawCoordinateSystem(transform);
            _rigidbody.AddForceAtPosition(-transform.up * 10, PinHoleCenter.position, ForceMode.Force);
            //Debug.DrawLine(ActivePin.position, ActivePin.velocity, Color.green);
        }
        else
        {
            _pushOutPin = false;
        }
    }

    public void Woke()
    {
        //SetSuctionFieldActive(false);

        // Gotta flip him up if he's pointing down
        //var faceDownAngle = Mathf.Clamp(Vector3.Angle(transform.up, Vector3.down), 0, 180);
        //var strength = (180 - faceDownAngle) / 180.0f;
        //_rigidbody.AddTorque(transform.right*strength*3000, ForceMode.Impulse);

        //Invoke("PushPinOut", 1);
        _pushOutPin = true;
    }

    //public void PushPinOut()
    //{
    //    if (ActivePin != null)
    //    {
    //        ActivePin.AddForce(ActivePin.rotation * -Vector3.up * 4, ForceMode.Impulse);
    //    }
    //}

    //private void SetSuctionFieldActive(bool active)
    //{
    //    foreach (var suctionField in SuctionFields)
    //    {
    //        suctionField.SetActive(active);
    //    }
    //}
}

public static class WilDebug
{
    public static void DrawCoordinateSystem(Transform gameObject)
    {
        Debug.DrawLine(gameObject.position, gameObject.position + gameObject.rotation * Vector3.up, Color.red);
        Debug.DrawLine(gameObject.position, gameObject.position + gameObject.rotation * Vector3.forward, Color.blue);
        Debug.DrawLine(gameObject.position, gameObject.position + gameObject.rotation * Vector3.left, Color.green);
    }
}