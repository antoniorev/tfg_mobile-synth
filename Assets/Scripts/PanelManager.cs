using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    private List<GameObject> _synths;
    private const string _scriptName = "Note";
    private List<string> _synthNames;


    //PRIVATE ATTRIBUTES: SECONDARY WAVE
    private GameObject _activeSecondaryWaveToggle;
    private const string _activeSecondaryWaveToggleName = "ActiveSecondaryWaveToggle";
    private GameObject _secondaryWaveInsidePanel;
    private const string _secondaryWaveInsidePanelName = "SecondaryWaveInsidePanel";

    //PRIVATE ATTRIBUTES: WAVES
    private GameObject _simetrySlider;
    private const string _simetrySliderName = "SliderSimetry";
    private GameObject _brightSlider;
    private const string _brightSliderName = "SliderBright";

    //PRIVATE ATTRIBUTES: FILTERS
    private GameObject _cutOffSlider;
    private const string _cutOffSliderName = "SliderCutOff";
    private GameObject _resonanceSlider;
    private const string _resonanceSliderName = "SliderResonance";

    //PRIVATE ATTRIBUTES: ENVELOPES
    private GameObject _pedalToggleButton;
    private const string _pedalToggleButtonName = "Pedal";
    private GameObject _pedalSlider;
    private const string _pedalSliderName = "SliderPedal";
    private GameObject _attackSlider;
    private const string _attackSliderName = "SliderAttack";
    private GameObject _sustainSlider;
    private const string _sustainSliderName = "SliderSustain";
    private GameObject _decaySlider;
    private const string _decaySliderName = "SliderDecay";

    //PRIVATE ATTRIBUTES: LFO
    private GameObject _depthSlider;
    private const string _depthSliderName = "SliderDepth";
    private GameObject _rateSlider;
    private const string _rateSliderName = "SliderRate";
    private GameObject _delaySlider;
    private const string _delaySliderName = "SliderDelay";

    public void Start()
    {
        _synthNames = new List<string>() { "Synth_C4", "Synth_C#4", "Synth_D4", "Synth_D#4", "Synth_E4", "Synth_F4", "Synth_F#4", "Synth_G4", "Synth_G#4", "Synth_A4", "Synth_A#4", "Synth_B4", "Synth_C5", "Synth_C#5", "Synth_D5", "Synth_D#5", "Synth_E5", "Synth_F5" };
        _synths = new List<GameObject>();

        AssignGameObject(out _pedalToggleButton, _pedalToggleButtonName);
        AssignGameObject(out _activeSecondaryWaveToggle, _activeSecondaryWaveToggleName);
        AssignGameObject(out _secondaryWaveInsidePanel, _secondaryWaveInsidePanelName);
        AssignGameObject(out _pedalSlider, _pedalSliderName);
        AssignGameObject(out _simetrySlider, _simetrySliderName);
        AssignGameObject(out _brightSlider, _brightSliderName);
        AssignGameObject(out _cutOffSlider, _cutOffSliderName);
        AssignGameObject(out _resonanceSlider, _resonanceSliderName);
        AssignGameObject(out _attackSlider, _attackSliderName);
        AssignGameObject(out _sustainSlider, _sustainSliderName);
        AssignGameObject(out _decaySlider, _decaySliderName);
        AssignGameObject(out _depthSlider, _depthSliderName);
        AssignGameObject(out _rateSlider, _rateSliderName);
        AssignGameObject(out _delaySlider, _delaySliderName);


        foreach (string name in _synthNames)
        {
            if (GameObject.Find(name) != null)
            {
                _synths.Add(GameObject.Find(name));
            }
        }
    }

    private void AssignGameObject(out GameObject gameObject, string gameObjectName)
    {
        if (GameObject.Find(gameObjectName) != null)
        {
            gameObject = GameObject.Find(gameObjectName);
        }
        else
            gameObject = null;
    }

    //PUBLIC METHODS: PEDAL
    public void SetPedalToAllNotes()
    {
        bool isPedalActive = _pedalToggleButton.GetComponent<Toggle>().isOn;
        _pedalSlider.GetComponent<Slider>().interactable = isPedalActive;
        foreach (GameObject synth in _synths)
        {
            synth.GetComponent<Note>().UpdatePedal(isPedalActive);
        }
    }

    public void ActiveSecondaryWave()
    {
        bool isSecondaryWaveActive = _activeSecondaryWaveToggle.GetComponent<Toggle>().isOn;
        var toggles = _secondaryWaveInsidePanel.GetComponentsInChildren(typeof(Toggle));
        var sliders = _secondaryWaveInsidePanel.GetComponentsInChildren(typeof(Slider));
        foreach (Toggle toggle in toggles)
        {
            toggle.interactable = isSecondaryWaveActive;
        }

        foreach (Slider slider in sliders)
        {
            slider.interactable = isSecondaryWaveActive;
        }

        foreach (GameObject synth in _synths)
        {
            synth.GetComponent<Note>().ActiveSecondaryOscilator(isSecondaryWaveActive);
        }
    }

    public void SetPedalValueToAllNotes()
    {
        float pedalValue = _pedalSlider.GetComponent<Slider>().value;
        foreach (GameObject synth in _synths)
        {
            synth.GetComponent<Note>().UpdatePedalValue(pedalValue);
        }
    }
    //PUBLIC METHODS: ENVELOPE
    public void SetAttackValueToAllNotes()
    {
        float attackValue = _attackSlider.GetComponent<Slider>().value;
        foreach (GameObject synth in _synths)
        {
            synth.GetComponent<Note>().UpdateAttackValue(attackValue);
        }
    }

    public void SetSustainValueToAllNotes()
    {
        float sustainValue = _sustainSlider.GetComponent<Slider>().value;
        if(sustainValue < 0.5f)
        {
            _decaySlider.GetComponent<Slider>().interactable = true;
        }
        else
        {
            _decaySlider.GetComponent<Slider>().interactable = false;
        }

        foreach (GameObject synth in _synths)
        {
            synth.GetComponent<Note>().UpdateSustainValue(sustainValue);
        }
    }

    public void SetDecayValueToAllNotes()
    {
        float decayValue = _decaySlider.GetComponent<Slider>().value;
        foreach (GameObject synth in _synths)
        {
            synth.GetComponent<Note>().UpdateDecayValue(decayValue);
        }
    }

    //PUBLIC METHODS: WAVES
    public void SetPrincipalOscilatorWave(bool wave)
    {
        _simetrySlider.GetComponent<Slider>().interactable = !wave;
        _brightSlider.GetComponent<Slider>().interactable = !wave;
        foreach (GameObject synth in _synths)
        {
                synth.GetComponent<Note>().UpdatePrincipalOscilatorWave(wave);
        }
    }

    public void SetSecondaryOscilatorWave(bool wave)
    {
        GameObject.Find("SecondarySliderSimetry").GetComponent<Slider>().interactable = !wave;
        GameObject.Find("SecondarySliderBright").GetComponent<Slider>().interactable = !wave;
        foreach (GameObject synth in _synths)
        {
            synth.GetComponent<Note>().UpdateSecondaryOscilatorWave(wave);
        }
    }

    public void SetSimetryValueToAllNotes(bool principalOscilator)
    {
        float simetryValue;
        foreach (GameObject synth in _synths)
        {
            if (principalOscilator)
            {
                simetryValue = _simetrySlider.GetComponent<Slider>().value;
                synth.GetComponent<Note>().UpdatePrincipalOscilatorSimetry(simetryValue);
            }
            else
            {
                simetryValue = GameObject.Find("SecondarySliderSimetry").GetComponent<Slider>().value;
                synth.GetComponent<Note>().UpdateSecondaryOscilatorSimetry(simetryValue);
            }
        }
    }

    public void SetBrightValueToAllNotes(bool principalOscilator)
    {
        float brightValue;
        foreach (GameObject synth in _synths)
        {
            if (principalOscilator)
            {
                brightValue = Mathf.Exp(_brightSlider.GetComponent<Slider>().value);
                synth.GetComponent<Note>().UpdatePrincipalOscilatorBright(brightValue);
            }
            else
            {
                brightValue = Mathf.Exp(GameObject.Find("SecondarySliderSimetry").GetComponent<Slider>().value);
                synth.GetComponent<Note>().UpdateSecondaryOscilatorBright(brightValue);
            }
        }
    }

    public void SetVolumeValueToAllNotes(bool principalOscilator)
    {
        var principalSlider = GameObject.Find("SliderVolume").GetComponent<Slider>();
        var secondarySlider = GameObject.Find("SecondarySliderVolume").GetComponent<Slider>();
        foreach (GameObject synth in _synths)
        {
            if (principalOscilator)
            {
                synth.GetComponent<Note>().UpdatePrincipalOscilatorVolume(principalSlider.value);
            }
            else
            {
                synth.GetComponent<Note>().UpdateSecondaryOscilatorVolume(secondarySlider.value);
            }
        }
    }

    //PUBLIC METHODS: FILTERS
    public void SetFilterToAllNotes(int selectedFilter)
    {
        bool isAudioHighPassFilterEnabled, isAudioLowPassFilterEnabled;
        switch (selectedFilter)
        {
            case 1:
                isAudioLowPassFilterEnabled = true;
                isAudioHighPassFilterEnabled = false;
                _cutOffSlider.GetComponent<Slider>().interactable = true;
                _resonanceSlider.GetComponent<Slider>().interactable = true;
                break;
            case 2:
                isAudioLowPassFilterEnabled = false;
                isAudioHighPassFilterEnabled = true;
                _cutOffSlider.GetComponent<Slider>().interactable = true;
                _resonanceSlider.GetComponent<Slider>().interactable = true;
                break;
            case 3:
                isAudioLowPassFilterEnabled = true;
                isAudioHighPassFilterEnabled = true;
                _cutOffSlider.GetComponent<Slider>().interactable = true;
                _resonanceSlider.GetComponent<Slider>().interactable = true;
                break;
            default:
                isAudioLowPassFilterEnabled = false;
                isAudioHighPassFilterEnabled = false;
                _cutOffSlider.GetComponent<Slider>().interactable = false;
                _resonanceSlider.GetComponent<Slider>().interactable = false;
                break;

        }

        foreach (GameObject synth in _synths)
        {
            synth.GetComponent<AudioHighPassFilter>().enabled = isAudioHighPassFilterEnabled;
            synth.GetComponent<AudioLowPassFilter>().enabled = isAudioLowPassFilterEnabled;
        }
    }

    public void SetCutOffFilterToAllNotes()
    {
        foreach (GameObject synth in _synths)
        {
            if (synth.GetComponent<AudioHighPassFilter>().enabled)
                synth.GetComponent<AudioHighPassFilter>().cutoffFrequency = _cutOffSlider.GetComponent<Slider>().value;
            if (synth.GetComponent<AudioLowPassFilter>().enabled)
                synth.GetComponent<AudioLowPassFilter>().cutoffFrequency = _cutOffSlider.GetComponent<Slider>().value;
        }
    }

    public void SetResonanceFilterToAllNotes()
    {
        foreach (GameObject synth in _synths)
        {
            if (synth.GetComponent<AudioHighPassFilter>().enabled)
                synth.GetComponent<AudioHighPassFilter>().highpassResonanceQ = _resonanceSlider.GetComponent<Slider>().value;
            if (synth.GetComponent<AudioLowPassFilter>().enabled)
                synth.GetComponent<AudioLowPassFilter>().lowpassResonanceQ = _resonanceSlider.GetComponent<Slider>().value;
        }
    }

    //PUBLIC METHODS: LFO
    public void SetDepthFilterToAllNotes()
    {
        foreach (GameObject synth in _synths)
        {
            synth.GetComponent<AudioChorusFilter>().depth = _depthSlider.GetComponent<Slider>().value;
        }
    }

    public void SetRateFilterToAllNotes()
    {
        foreach (GameObject synth in _synths)
        {
            synth.GetComponent<AudioChorusFilter>().rate = _rateSlider.GetComponent<Slider>().value;
        }
    }

    public void SetDelayFilterToAllNotes()
    {
        foreach (GameObject synth in _synths)
        {
            synth.GetComponent<AudioChorusFilter>().delay = _delaySlider.GetComponent<Slider>().value;
        }
    }
}
