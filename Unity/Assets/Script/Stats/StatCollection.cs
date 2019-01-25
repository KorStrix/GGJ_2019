using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatCollection
{
    private List<Stat> _stats;

    public List<Stat> Stats
    {
        get
        {
            if (_stats == null)
            {
                _stats = new List<Stat>();
            }
            return _stats;
        }
    }

    public Stat CreateStat(StatType type, int v, float fv)
    {
        Stat s;
        s = new Stat(type, v, fv);
        Stats.Add(s);
        return s;
    }

    public MutableStat CreateMutableStat(StatType type, int v, float fv)
    {
        MutableStat s;
        s = new MutableStat(type, v, fv);
        Stats.Add(s);
        return s;
    }
}