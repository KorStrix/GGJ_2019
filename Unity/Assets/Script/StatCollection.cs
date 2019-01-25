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

    protected virtual void ConfigureStats()
    {

    }

    public Stat CreateStat(string name, int v, float fv)
    {
        Stat s;
        s = new Stat(name, v, fv);
        Stats.Add(s);
        return s;
    }

    public MutableStat CreateMutableStat(string name, int v, float fv)
    {
        MutableStat s;
        s = new MutableStat(name, v, fv);
        Stats.Add(s);
        return s;
    }
}