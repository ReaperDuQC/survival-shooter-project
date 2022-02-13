using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightSystem : MonoBehaviour
{
    [SerializeField] Light _light;
    [SerializeField] float _nightDuration;
    [SerializeField] bool _isNight = false;
    [SerializeField] bool _useCycle = true;
    float minLightIntensity = 0;
    float maxLightIntensity = 1.0f;

    void Update()
    {
        if (_useCycle)
        {
            float lightIntensity = 0.5f + Mathf.Sin(Time.time * 2.0f * Mathf.PI / _nightDuration) / 2.0f;
            if (_isNight != (lightIntensity < 0.3))
            {
                _isNight = !_isNight;
            }
            _light.intensity = Mathf.Lerp(minLightIntensity, maxLightIntensity, lightIntensity);
        }
    }

    public bool IsNight()
    {
        return _isNight;
    }
}
