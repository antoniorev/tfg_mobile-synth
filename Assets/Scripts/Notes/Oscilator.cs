using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscilator : MonoBehaviour
{
    private float _simetry = 0.0f;
    private float _bright = 1.0f;
    private float _volume = 0.5f;
    private bool _isSineWave = true;

    public void SetSimetry(float simetry)
    {
        _simetry = simetry;
    }

    public void SetBright(float bright)
    {
        _bright = bright;
    }
    
    public void SetVolume(float volume)
    {
        _volume = volume;
    }

    public void SetWave(bool isSineWave)
    {
        _isSineWave = isSineWave;
    }

    public float GenerateWave(float gain, float phase)
    {
        if (_isSineWave)
            return SineWave(gain, phase);
        else
            return SawToothWaveWithAdjustableSimetry(gain, phase);
    }

    public float SineWave(float gain, float phase)
    {
        return 2.0f * gain * _volume * Mathf.Sin(phase);
    }

    //Triangle
    //(1 / π) * asin(1 / sin(((x*200* 4 * π / 48000)%(π* 2)) /2))

    //SawTooth
    //(-1 / π) * atan(1 / tan(((x*200* 2 * π / 48000)%(π* 2)) /2))

    //atan = 1/tan
    //1/tan = 0.5*cos(1 / tan(((x*200* 2 * π / 48000)%(π* 2)) /2))/sen(1 / tan(((x*200* 2 * π / 48000)%(π* 2)) /2))

    //tan = sen(1 / tan(((x*200* 2 * π / 48000)%(π* 2)) /2))/0.5*cos(1 / tan(((x*200* 2 * π / 48000)%(π* 2)) /2))

    public float SawToothWave(float gain, float phase)
    {
        return -2.0f * gain * _volume / Mathf.PI * Mathf.Atan(1.0f / Mathf.Tan(phase / 2.0f));
    }

    public float SawToothWaveWithAdjustableSimetry(float gain, float phase)
    {
        float sinFunction = (2.0f   * gain * _simetry          / Mathf.PI) * Mathf.Asin(Mathf.Sin(phase));
        float tanFunction = (-2.0f  * gain * (1.0f - _simetry) / Mathf.PI) * Mathf.Atan(_bright * Mathf.Cos(phase / 2.0f) / Mathf.Sin(phase / 2.0f));
        return _volume * (sinFunction + tanFunction);
    }
}
