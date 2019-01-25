using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat
{
    private StatType _type;
    //스탯은 정수 또는 부동소수점 값을 갖는다.
    protected int _value;
    protected float _fvalue;
    //스탯에는 증가 버프와 배수 버프가 있다.
    //증가 버프 적용 후 배수 버프가 적용된다.
    protected Dictionary<StatType, int> _addItemBuff;
    protected Dictionary<StatType, int> _addBuff;
    protected Dictionary<StatType, float> _multiItemBuff;
    protected Dictionary<StatType, float> _multiBuff;

    public Stat (StatType type, int value, float fvalue)
    {
        Type = type;
        Value = value;
        FValue = fvalue;
    }

    public StatType Type
    {
        get { return _type; }
        set { _type = value; }
    }

    public int Value
    {
        get { return StatClipper(); }
        set { _value = value; }
    }

    public float FValue
    {
        get { return FloatStatClipper(); }
        set { _fvalue = value; }
    }

    public Dictionary<StatType, int> AddItemBuff
    {
        get
        {
            if (_addItemBuff == null)
            {
                _addItemBuff = new Dictionary<StatType, int>();
            }
            return _addItemBuff;
        }
    }

    public Dictionary<StatType, int> AddBuff
    {
        get
        {
            if (_addBuff == null)
            {
                _addBuff = new Dictionary<StatType, int>();
            }
            return _addBuff;
        }
    }

    public Dictionary<StatType, float> MultiItemBuff
    {
        get
        {
            if (_multiItemBuff == null)
            {
                _multiItemBuff = new Dictionary<StatType, float>();
            }
            return _multiItemBuff;
        }
    }

    public Dictionary<StatType, float> MultiBuff
    {
        get
        {
            if (_multiBuff == null)
            {
                _multiBuff = new Dictionary<StatType, float>();
            }
            return _multiBuff;
        }
    }

    public void SetAddItemBuff(StatType buffType, int value)
    {
        if (_addItemBuff.ContainsKey(buffType))
        {
            return;
        }
        _addItemBuff.Add(buffType, value);
    }

    public void SetAddBuff(StatType buffType, int value)
    {
        if (_addBuff.ContainsKey(buffType))
        {
            return;
        }
        _addBuff.Add(buffType, value);
    }

    public void SetMultiItemBuff(StatType buffType, float value)
    {
        if (_multiItemBuff.ContainsKey(buffType))
        {
            return;
        }
        _multiItemBuff.Add(buffType, value);
    }

    public void SetMultiBuff(StatType buffType, float value)
    {
        if (_multiBuff.ContainsKey(buffType))
        {
            return;
        }
        _multiBuff.Add(buffType, value);
    }

    public void RemoveAddItemBuff(StatType buffType)
    {
        if (_addItemBuff.ContainsKey(buffType))
        {
            _addItemBuff.Remove(buffType);
        }
    }

    public void RemoveAddBuff(StatType buffType)
    {
        if (_addBuff.ContainsKey(buffType))
        {
            _addBuff.Remove(buffType);
        }
    }

    public void RemoveMultiItemBuff(StatType buffType)
    {
        if (_multiItemBuff.ContainsKey(buffType))
        {
            _multiItemBuff.Remove(buffType);
        }
    }

    public void RemoveMultiBuff(StatType buffType)
    {
        if (_multiBuff.ContainsKey(buffType))
        {
            _multiBuff.Remove(buffType);
        }
    }

    public void ClearAddItemBuff()
    {
        _addItemBuff.Clear();
    }

    public void ClearAddBuff()
    {
        _addBuff.Clear();
    }

    public void ClearMultiItemBuff()
    {
        _multiItemBuff.Clear();
    }

    public void ClearMultiBuff()
    {
        _multiBuff.Clear();
    }

    public virtual int StatClipper()
    {
        int v = _value;
        foreach (KeyValuePair<StatType, int> i in AddItemBuff)
        {
            v += i.Value;
        }
        foreach (KeyValuePair<StatType, int> j in AddBuff)
        {
            v += j.Value;
        }
        float w = (float)v;
        foreach (KeyValuePair<StatType, float> k in MultiItemBuff)
        {
            w *= k.Value;
        }
        foreach (KeyValuePair<StatType, float> l in MultiBuff)
        {
            w *= l.Value;
        }
        w = Clip(w);
        return (int)w;
    }

    public virtual float FloatStatClipper()
    {
        float v = _fvalue;
        foreach (KeyValuePair<StatType, int> i in AddItemBuff)
        {
            v += i.Value;
        }
        foreach (KeyValuePair<StatType, int> j in AddBuff)
        {
            v += j.Value;
        }
        foreach (KeyValuePair<StatType, float> k in MultiItemBuff)
        {
            v *= k.Value;
        }
        foreach (KeyValuePair<StatType, float> l in MultiBuff)
        {
            v *= l.Value;
        }
        v = Clip(v);
        return v;
    }

    public float Clip(float f)
    {
        return Mathf.Clamp(f, 0.0f, 65536f);
    }
}
