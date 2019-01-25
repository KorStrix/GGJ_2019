using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutableStat : Stat
{
    public int maxStat;
    public float maxFStat;

    public MutableStat (StatType type, int v, float fv) : base(type, v, fv)
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
        foreach (KeyValuePair<BuffType, int> i in AddItemBuff)
        {
            v += i.Value;
        }
        foreach (KeyValuePair<BuffType, int> j in AddBuff)
        {
            v += j.Value;
        }
        float w = (float)v;
        foreach (KeyValuePair<BuffType, float> k in MultiItemBuff)
        {
            w *= k.Value;
        }
        foreach (KeyValuePair<BuffType, float> l in MultiBuff)
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
        foreach (KeyValuePair<BuffType, int> i in AddItemBuff)
        {
            v += i.Value;
        }
        foreach (KeyValuePair<BuffType, int> j in AddBuff)
        {
            v += j.Value;
        }
        foreach (KeyValuePair<BuffType, float> k in MultiItemBuff)
        {
            v *= k.Value;
        }
        foreach (KeyValuePair<BuffType, float> l in MultiBuff)
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
