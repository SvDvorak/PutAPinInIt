using UnityEngine;

public class GrenadeSpawner : MonoBehaviour
{
    public GameObject GrenadeTemplate;
    public float SpawnTime;
    public Transform Spawn1;
    public Transform Spawn2;

    private float _timer;

    public void Start()
    {
        _timer = Time.time + SpawnTime;
    }

    public void Update()
    {
        if (_timer < Time.time)
        {
            Invoke("Spawn", 0);
            _timer = Time.time + SpawnTime;
        }
    }

    public void Spawn()
    {
        var spawn = Random.value > 0.5f ? Spawn1 : Spawn2;

        var grenade = Instantiate(GrenadeTemplate, spawn.position, Random.rotation);
        var rigidBody = grenade.GetComponent<Rigidbody>();

        rigidBody.AddForce((spawn.right + Vector3.up)*5, ForceMode.Impulse);
        rigidBody.AddTorque(Random.insideUnitSphere*5, ForceMode.Impulse);
    }
}
