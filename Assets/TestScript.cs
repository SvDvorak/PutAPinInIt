using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    void Start()
    {
        var rigidbody = GetComponent<Rigidbody>();
        rigidbody.AddForce(rigidbody.rotation * -Vector3.up*4, ForceMode.Impulse);
    }

    void Update()
    {

    }
}
