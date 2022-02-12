using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightSystem : MonoBehaviour
{
    [SerializeField] bool _isNight = false;
    [SerializeField] bool _useCycle = true;
    [SerializeField] float _nightDuration;
    float _currentDuration;
    [SerializeField] Light _light;

    void Update()
    {
        if (_useCycle)
        {
            _currentDuration += Time.deltaTime;
            float ratio = _currentDuration / _nightDuration;

            if (ratio > 1f)
            {
                _currentDuration = 0f;
                _isNight = !_isNight;
            }
        }
    }

    public bool IsNight()
    {
        return _isNight;
    }
}
