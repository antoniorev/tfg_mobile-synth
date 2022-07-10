using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Envelope : MonoBehaviour
{
    float _amplitude;
    float _sustain;

    double _attackTime;
    double _decayTime;

    double _onTime;

    public Envelope()
    {
        _amplitude = 0.5f;
        _sustain = 0.5f;
        _attackTime = 0.0;
        _decayTime = 0.0;

        _onTime = 0.0;
    }

    public float GetGain(double time)
    {
        double lifeTime = time - _onTime;

        if(lifeTime < _attackTime)
        {
            return (float)(lifeTime / _attackTime) * _amplitude;
        }
        else if (lifeTime < _decayTime)
        {
            return (float)((lifeTime - _attackTime) / _decayTime) * (_sustain - _amplitude) + _amplitude;
        }
        else
        {
            return _sustain;
        }            
    }

    public void SetLifeTime(double time)
    {
        _onTime = time;
    }

    public void SetAttack(float attack)
    {
        _attackTime = attack;
    }

    public void SetDecay(float decay)
    {
        _decayTime = decay;
    }

    public void SetSustain(float sustain)
    {
        _sustain = sustain;
    }
}
