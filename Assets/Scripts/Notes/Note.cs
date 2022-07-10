using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Note : MonoBehaviour
{
    public double _frequency;
    private double _increment;
    private double _phase;
    private double _sampling_frequency = 48000.0;

    private Oscilator _principalOscilator = new(), _secondaryOscilator = new();
    private Envelope _envelope = new();

    private float _gain = 0.0f;
    private bool _soundNote = false;
    private int _secondaryOscilatorActive = 0;

    private bool _pedal = false;
    private float _pedalValue = 0.01f;

    public void OnAudioFilterRead(float[] data, int channels)
    {
        _increment = _frequency * 2.0 * Mathf.PI / _sampling_frequency;
        for (int i = 0; i < data.Length; i += channels)
        {
            _phase += _increment;
            if (_gain > 0.0f)
                data[i] =                      ((float)_principalOscilator.GenerateWave(_gain, (float)_phase) + 
                    _secondaryOscilatorActive * (float)_secondaryOscilator.GenerateWave(_gain, (float)_phase));
            else
                data[i] = 0.0f;

            if (channels == 2)
                data[i + 1] = data[i];
            
            _phase = _phase % (Mathf.PI * 2);
        }
    }

    //SOUND AND PEDAL FUNCTIONS
    public void Update()
    {
        //REVISAR
        if (_soundNote)
        {
            _gain = _envelope.GetGain(Time.timeAsDouble);
        }
        else if (_gain < 0.0f || !_pedal)
            _gain = 0.0f;
        else if (_gain > 0.0f)
            _gain -= _pedalValue;
    }

    public void SoundNote()
    {
        if (Input.GetMouseButton(0))
        {
            _soundNote = true;
            _envelope.SetLifeTime(Time.timeAsDouble);
        }
    }

    public void StopNote()
    {
        _soundNote = false;
    }

    public void UpdatePedal(bool pedal)
    {
        _pedal = pedal;
    }

    public void UpdatePedalValue(float pedalValue)
    {
        _pedalValue = pedalValue;
    }

    //ENVELOPE FUNCTIONS
    public void UpdateAttackValue(float attackValue)
    {
        _envelope.SetAttack(attackValue);
    }

    public void UpdateSustainValue(float sustainValue)
    {
        _envelope.SetSustain(sustainValue);
    }

    public void UpdateDecayValue(float decayValue)
    {
        _envelope.SetDecay(decayValue);
    }

    //PRINCIPAL OSCILATOR FUNCTIONS
    public void UpdatePrincipalOscilatorWave(bool wave)
    {
        _principalOscilator.SetWave(wave);
    }

    public void UpdatePrincipalOscilatorSimetry(float simetry)
    {
        _principalOscilator.SetSimetry(simetry);
    }

    public void UpdatePrincipalOscilatorBright(float bright)
    {
        _principalOscilator.SetBright(bright);
    }

    public void UpdatePrincipalOscilatorVolume(float volume)
    {
        _principalOscilator.SetVolume(volume);
    }

    //SECONDARY OSCILATOR FUNCTIONS
    public void ActiveSecondaryOscilator(bool active)
    {
        if (active)
            _secondaryOscilatorActive = 1;
        else
            _secondaryOscilatorActive = 0;
    }

    public void UpdateSecondaryOscilatorWave(bool wave)
    {
        _secondaryOscilator.SetWave(wave);
    }

    public void UpdateSecondaryOscilatorSimetry(float simetry)
    {
        _secondaryOscilator.SetSimetry(simetry);
    }

    public void UpdateSecondaryOscilatorBright(float bright)
    {
        _secondaryOscilator.SetBright(bright);
    }

    public void UpdateSecondaryOscilatorVolume(float volume)
    {
        _secondaryOscilator.SetVolume(volume);
    }
}
