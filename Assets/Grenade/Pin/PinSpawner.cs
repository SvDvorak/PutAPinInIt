using System.Collections.Generic;
using UnityEngine;

public class PinSpawner : MonoBehaviour
{
    public GameObject PinTemplate;
    public Transform Spawn1;
    public Transform Spawn2;
    public float Force;

    private readonly List<Rigidbody> _grenades = new List<Rigidbody>();
    private readonly List<Rigidbody> _pins = new List<Rigidbody>();

    private float _missingPinsTimer;
    private Transform _spawnParent;

    public void Start()
    {
        _spawnParent = GameObject.Find("stuff").transform;
    }

    public void Update()
    {
        var expectedPins = 1 + (int)(_grenades.Count*0.4f);
        if (_pins.Count - _grenades.Count < expectedPins)
        {
            if (_missingPinsTimer > 2)
            {
                Spawn();
                _missingPinsTimer = -6;
            }

            _missingPinsTimer += Time.deltaTime;
        }
        else
        {
            _missingPinsTimer = 0;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        var layer = other.attachedRigidbody.gameObject.layer;

        if (layer == LayerMask.NameToLayer("Grenade") && !_grenades.Contains(other.attachedRigidbody))
            _grenades.Add(other.attachedRigidbody);

        if (layer == LayerMask.NameToLayer("Pin") && !_pins.Contains(other.attachedRigidbody))
            _pins.Add(other.attachedRigidbody);
    }

    public void OnTriggerExit(Collider other)
    {
        var layer = other.attachedRigidbody.gameObject.layer;

        if (layer == LayerMask.NameToLayer("Grenade"))
            _grenades.Remove(other.attachedRigidbody);

        if (layer == LayerMask.NameToLayer("Pin"))
            _pins.Remove(other.attachedRigidbody);
    }

    [ExposeMethodInEditor]
    public void Spawn()
    {
        var spawn = Random.value > 0.5f ? Spawn1 : Spawn2;

        var pin = Instantiate(PinTemplate, spawn.position, Quaternion.identity);
        pin.transform.parent = _spawnParent;
        var rigidBody = pin.GetComponentInChildren<Rigidbody>();

        rigidBody.AddForce((spawn.right + Vector3.up) * Force, ForceMode.Impulse);
        rigidBody.AddTorque(Random.insideUnitSphere, ForceMode.Impulse);
    }
}
