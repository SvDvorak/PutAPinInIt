using Assets;
using UnityEngine;

public class GrenadeLogic : MonoBehaviour
{
    public bool StartWithPin;
    public Transform PinHoleCenter;
    public GameObject Face;
    public GameObject SpoonTemplate;
    public GameObject PinTemplate;
    public GameObject ExplosionPrefab;

    public float ExplodeTime;
    [HideInInspector]
    public bool TimerActive;
    public bool HasExploded;
    private float _explodeTimer;

    public float RewokeDelay;
    private float _rewokeTimer;
    private bool _firstTimeWoke = true;

    private readonly Vector3 _spoonOffset = new Vector3(0, -0.62f, 1.39f);
    private readonly Vector3 _spoonRotation = new Vector3(0, 0, 180);
    private FixedJoint _spoonJoint;
    private Rigidbody _spoonRigidbody;

    private Rigidbody _rigidbody;
    public Rigidbody ActivePin;
    public Rigidbody PinOnTheWayInShaft;
    public bool IsAlive { get { return ActivePin == null && _rewokeTimer < Time.time; } }

    private bool _pushOutPin;
    private SoundShotPlayer _soundShotPlayer;

    public void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        if (StartWithPin)
        {
            var spawnParent = GameObject.Find("stuff").transform;
            var pin = Instantiate(PinTemplate, PinHoleCenter.position + PinHoleCenter.rotation * new Vector3(0, 1, 0), PinHoleCenter.rotation * Quaternion.Euler(-90, 0, 0));
            pin.transform.parent = spawnParent;

            var spoon = Instantiate(SpoonTemplate, transform.position + transform.rotation * _spoonOffset, transform.rotation*Quaternion.Euler(_spoonRotation));
            spoon.transform.parent = spawnParent;
            _spoonJoint = gameObject.AddComponent<FixedJoint>();
            _spoonRigidbody = spoon.GetComponent<Rigidbody>();
            _spoonJoint.connectedBody = _spoonRigidbody;
        }

        _soundShotPlayer = GetComponent<SoundShotPlayer>();
        _soundShotPlayer.PlaySound("Spawn");

        _rewokeTimer = Time.time + RewokeDelay;
    }

    public void Update()
    {
        Face.SetActive(IsAlive);

        if (IsAlive)
        {
            var toCamera = Vector3.SignedAngle(transform.forward, Vector3.back, Vector3.up);
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
            _soundShotPlayer.PlayVoice("Woke");
            if (_firstTimeWoke)
            {
                _firstTimeWoke = false;
                Destroy(_spoonJoint);
                _soundShotPlayer.PlaySound("WokeSpoonSound");
                _spoonRigidbody.AddForce(-transform.up * 3, ForceMode.Impulse);
            }
            else
            {
                _soundShotPlayer.PlaySound("WokeSound");
            }

            _explodeTimer = Time.time + ExplodeTime;
        }
        if (!IsAlive && TimerActive)
        {
            TimerActive = false;
            _soundShotPlayer.PlaySound("PinLocked");
            _rewokeTimer = Time.time + RewokeDelay;
        }

        if (!HasExploded && TimerActive && _explodeTimer < Time.time)
        {
            HasExploded = true;
            Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
            _soundShotPlayer.PlayVoice("ExplosionVoice");
            _soundShotPlayer.PlaySound("Explosion");
            GameState.GameOver = true;
        }

        if (_pushOutPin && !IsAlive && ActivePin != null)
        {
            ActivePin.AddForce(ActivePin.rotation * Vector3.forward * 6, ForceMode.Force);
            _rigidbody.AddForceAtPosition(-transform.up * 10, PinHoleCenter.position, ForceMode.Force);
        }
        else
        {
            _pushOutPin = false;
        }
    }

    public void Woke()
    {
        _pushOutPin = true;
    }
}