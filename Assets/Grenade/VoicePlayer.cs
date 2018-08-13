using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VoicePlayer : MonoBehaviour
{
    public float TimeBetweenVoiceLines;
    public float TimeVariation;

    private float _nextVoiceTimer;
    private GrenadesController _grenadesController;
    private List<GrenadeLogic> Grenades { get { return _grenadesController.Grenades; } }

    public void Start()
    {
        _grenadesController = gameObject.GetComponent<GrenadesController>();
        SetNextVoiceTime();
    }

    public void Update()
    {
        if (_nextVoiceTimer < Time.time)
        {
            var grenade = Grenades.Where(x => x.IsAlive).RandomOrDefault();
            if (grenade != null)
            {
                grenade.Talk();
                SetNextVoiceTime();
            }
        }
    }

    private void SetNextVoiceTime()
    {
        _nextVoiceTimer = Time.time + TimeBetweenVoiceLines + Random.Range(-TimeVariation, TimeVariation) / 2f;
    }
}
