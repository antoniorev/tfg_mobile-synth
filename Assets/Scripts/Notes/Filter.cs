using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Filter : MonoBehaviour
{
    private FilterType _filterType = FilterType.HighFilter;
    private double _cutOff = 0.1;
    private double _resonance = 2.0;
    private const double _maxFrequency = 20000.0;

    public Filter()
    {
    }

    public void SetFilterType(FilterType filterType)
    {
        _filterType = filterType;
    }

    public void SetCutOff(double cutOff)
    {
        _cutOff = cutOff;
    }

    public void SetResonance(double resonance)
    {
        _resonance = resonance;
    }

    public double ApplyFilter(double freq)
    {
        double filterResult;
        switch (_filterType)
        {
            case FilterType.LowFilter:
                filterResult = ApplyLowFilter(freq);
                break;
            case FilterType.HighFilter:
                filterResult = ApplyHighFilter(freq);
                break;
            case FilterType.BandFilter:
                filterResult = ApplyBandFilter(freq);
                break;
            default:
                filterResult = freq;
                break;
        }

        return filterResult;
    }

    private double ApplyLowFilter(double freq)
    {
        if (1/freq > _cutOff*20000)
            return 0;
        else
            return freq;
    }

    private double ApplyHighFilter(double freq)
    {
        if (freq < _cutOff)
            return 0;
        else
            return freq;
    }

    private double ApplyBandFilter(double freq)
    {
        return freq;
    }
}


public enum FilterType
{
    None = 0,
    LowFilter = 1,
    HighFilter = 2,
    BandFilter = 3
}