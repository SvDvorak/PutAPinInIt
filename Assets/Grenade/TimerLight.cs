using System.Linq;
using UnityEngine;

public class TimerLight : MonoBehaviour
{
    private GrenadeLogic _grenadeLogic;
    private bool _showingLight;
    private float _startOffset;
    private Light[] _lights;
    private float _maxLight;

    public float MinFrequency;
    public float MaxFrequency;
    public float FrequencyIncrements;

    public float[] FrequencySteps;

    public void Start()
    {
        _grenadeLogic = GetComponentInParent<GrenadeLogic>();
        _lights = GetComponentsInChildren<Light>();
        _maxLight = _lights.Average(x => x.intensity);
    }

    public void Update()
    {
        if (_grenadeLogic.TimerActive && !_showingLight)
            StartLight();
        else if (!_grenadeLogic.TimerActive && _showingLight)
            StopLight();

        var timeElapsed = Time.time-_startOffset;
        var index = (int)(FrequencySteps.Length * Mathf.Min(timeElapsed / _grenadeLogic.ExplodeTime, 0.99f));

        foreach (var light in _lights)
        {
            if (_grenadeLogic.HasExploded)
                light.intensity = _maxLight;
            else
                light.intensity = Mathf.Abs(Mathf.Sin(timeElapsed*FrequencySteps[index]))*_maxLight;
        }
    }

    private void StartLight()
    {
        _showingLight = true;
        _startOffset = Time.time;

        foreach (var light in _lights)
        {
            light.enabled = true;
        }
    }

    private void StopLight()
    {
        _showingLight = false;

        foreach (var light in _lights)
        {
            light.enabled = false;
        }
    }
}
