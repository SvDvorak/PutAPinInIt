using System;
using System.Collections.Generic;
using UnityEngine;

public struct PinInVicinity
{
    public Collider Collider;
    public PinState State;
}

public class PinHoleSuction : MonoBehaviour
{
    public Transform HoleCenter;
    public float Force;

    public int Pins;
    public Dictionary<Rigidbody, PinInVicinity> PinsInVicinity = new Dictionary<Rigidbody, PinInVicinity>();
    private GrenadeLogic _grenadeLogic;

    public void Start()
    {
        _grenadeLogic = GetComponentInParent<GrenadeLogic>();
    }

    public void FixedUpdate()
    {
        Pins = PinsInVicinity.Count;
        if (!_grenadeLogic.IsAlive)
            return;

        foreach (var pinInVicinity in PinsInVicinity)
        {
            if (!pinInVicinity.Value.State.IsOnFinger)
                return;

            var suctionPoint = _grenadeLogic.PinOnTheWayInShaft == pinInVicinity.Key ? HoleCenter.position : transform.position;
            var point = pinInVicinity.Value.Collider.ClosestPointOnBounds(suctionPoint);
            var diff = suctionPoint - point;
            Debug.DrawLine(point, suctionPoint + diff * 10, Color.red);
            pinInVicinity.Key.angularVelocity *= 0.2f;
            pinInVicinity.Key.AddForce((suctionPoint - point).normalized * Force);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        var pinInVicinity = new PinInVicinity
        {
            Collider = other.gameObject.GetComponent<Collider>(),
            State = other.gameObject.GetComponentInParent<PinState>()
        };

        PinsInVicinity.Add(other.attachedRigidbody, pinInVicinity);
    }

    public void OnTriggerExit(Collider other)
    {
        PinsInVicinity.Remove(other.attachedRigidbody);
    }
}
