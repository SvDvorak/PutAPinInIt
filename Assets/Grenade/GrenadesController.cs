using System.Collections.Generic;
using System.Linq;
using Assets;
using UnityEngine;

public class GrenadesController : MonoBehaviour
{
    public GameObject GrenadeTemplate;
    public float SpawnTime;
    public float WokeTime;
    public float Force;
    public Transform Spawn1;
    public Transform Spawn2;

    private readonly List<GrenadeLogic> _grenades = new List<GrenadeLogic>();

    private float _spawnTimer;
    private float _wokeTimer;
    private Transform _spawnParent;

    public void Start()
    {
        _spawnTimer = 0;
        _wokeTimer = Time.time + WokeTime / 3.0f;

        _spawnParent = GameObject.Find("stuff").transform;
        _grenades.AddRange(_spawnParent.GetComponentsInChildren<GrenadeLogic>());
        GameState.GrenadesSpawned = _grenades.Count;
    }

    public void Update()
    {
        if (GameState.GameOver)
            return;

        if (_spawnTimer < Time.time)
        {
            Spawn();
            _spawnTimer = Time.time + SpawnTime;
        }

        if (_wokeTimer < Time.time)
        {
            Woke();
            _wokeTimer = Time.time + WokeTime;
        }
    }

    [ExposeMethodInEditor]
    public void Spawn()
    {
        var spawn = Random.value > 0.5f ? Spawn1 : Spawn2;

        var grenade = Instantiate(GrenadeTemplate, spawn.position, Quaternion.identity);
        grenade.transform.parent = _spawnParent;
        var rigidBody = grenade.GetComponent<Rigidbody>();
        var grenadeLogic = grenade.GetComponent<GrenadeLogic>();
        _grenades.Add(grenadeLogic);
        GameState.GrenadesSpawned += 1;

        rigidBody.AddForce((spawn.right + Vector3.up) * Force, ForceMode.Impulse);
        rigidBody.AddTorque(Random.insideUnitSphere * 3, ForceMode.Impulse);
    }

    [ExposeMethodInEditor]
    public void Woke()
    {
        var grenade = _grenades
            .Where(x => !x.IsAlive)
            .RandomOrDefault();

        if (grenade != null)
            grenade.Woke();
    }
}

public static class EnumerableExtensions
{
    public static T RandomOrDefault<T>(this IEnumerable<T> enumerable) where T : class
    {
        var list = enumerable.ToList();
        return list.Count == 0 ? null : list[Random.Range(0, list.Count)];
    }
}