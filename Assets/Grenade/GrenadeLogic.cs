using Assets;
using UnityEngine;

public class GrenadeLogic : MonoBehaviour
{
    public bool StartWithPin;
    public Transform PinHoleCenter;
    public GameObject Face;
    public GameObject PinTemplate;
    public GameObject ExplosionPrefab;

    public float ExplodeTime;
    [HideInInspector]
    public bool TimerActive;
    public bool HasExploded;
    private float _explodeTimer;

    public float RewokeDelay;
    private float _rewokeTimer;

    private Rigidbody _rigidbody;
    public Rigidbody ActivePin;
    public bool IsAlive { get { return ActivePin == null && _rewokeTimer < Time.time; } }

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
        Face.SetActive(IsAlive);

        if (IsAlive)
        {
            var toCamera = Vector3.SignedAngle(transform.forward, Vector3.back, Vector3.up);
            Debug.DrawRay(transform.position, transform.position + transform.forward, Color.white);
            Debug.DrawRay(transform.position, transform.position + Vector3.back, Color.green);
            if (Mathf.Abs(toCamera) > 90)
                _rigidbody.AddTorque(Vector3.up * toCamera * 0.05f, ForceMode.Force);

            var toUpAxis = Vector3.Cross(Vector3.up, transform.up);
            var faceUp = Vector3.SignedAngle(Vector3.up, transform.up, toUpAxis);
            if (Mathf.Abs(faceUp) > 45)
                _rigidbody.AddRelativeTorque(toUpAxis * faceUp * 0.05f, ForceMode.Force);
        }

        _rigidbody.angularVelocity *= 0.95f;


        if (IsAlive && !TimerActive)
        {
            TimerActive = true;
            _explodeTimer = Time.time + ExplodeTime;
        }
        if (!IsAlive && TimerActive)
        {
            TimerActive = false;
            _rewokeTimer = Time.time + RewokeDelay;
        }

        if (!HasExploded && TimerActive && _explodeTimer < Time.time)
        {
            HasExploded = true;
            Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
            GameState.GameOver = true;
        }

        if (_pushOutPin && !IsAlive && ActivePin != null)
        {
            ActivePin.AddForce(ActivePin.rotation * -Vector3.up * 10, ForceMode.Force);
            _rigidbody.AddForceAtPosition(-transform.up * 10, PinHoleCenter.position, ForceMode.Force);
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