using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutableStat : Stat
{
    public int maxStat;
    public float maxFStat;

    public MutableStat (string name, int v, float fv) : base(name, v, fv)
    {
        //
    }

    public void Awake()
    {
        maxStat = _value;
        maxFStat = _fvalue;
    }

    public override int StatClipper()
    {
        int v = _value;
        foreach (KeyValuePair<string, int> i in AddItemBuff)
        {
            v += i.Value;
        }
        foreach (KeyValuePair<string, int> j in AddBuff)
        {
            v += j.Value;
        }
        float w = (float)v;
        foreach (KeyValuePair<string, float> k in MultiItemBuff)
        {
            w *= k.Value;
        }
        foreach (KeyValuePair<string, float> l in MultiBuff)
        {
            w *= l.Value;
        }
        if (w > maxFStat)
        {
            w = maxFStat;
        }
        w = Clip(w);
        return (int)w;
    }

    public override float FloatStatClipper()
    {
        float v = _fvalue;
        foreach (KeyValuePair<string, int> i in AddItemBuff)
        {
            v += i.Value;
        }
        foreach (KeyValuePair<string, int> j in AddBuff)
        {
            v += j.Value;
        }
        foreach (KeyValuePair<string, float> k in MultiItemBuff)
        {
            v *= k.Value;
        }
        foreach (KeyValuePair<string, float> l in MultiBuff)
        {
            v *= l.Value;
        }
        if (v > maxFStat)
        {
            v = maxFStat;
        }
        v = Clip(v);
        return v;
    }
}
