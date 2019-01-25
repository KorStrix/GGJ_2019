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
    protected Dictionary<BuffType, int> _addItemBuff;
    protected Dictionary<BuffType, int> _addBuff;
    protected Dictionary<BuffType, float> _multiItemBuff;
    protected Dictionary<BuffType, float> _multiBuff;

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

    public Dictionary<BuffType, int> AddItemBuff
    {
        get
        {
            if (_addItemBuff == null)
            {
                _addItemBuff = new Dictionary<BuffType, int>();
            }
            return _addItemBuff;
        }
    }

    public Dictionary<BuffType, int> AddBuff
    {
        get
        {
            if (_addBuff == null)
            {
                _addBuff = new Dictionary<BuffType, int>();
            }
            return _addBuff;
        }
    }

    public Dictionary<BuffType, float> MultiItemBuff
    {
        get
        {
            if (_multiItemBuff == null)
            {
                _multiItemBuff = new Dictionary<BuffType, float>();
            }
            return _multiItemBuff;
        }
    }

    public Dictionary<BuffType, float> MultiBuff
    {
        get
        {
            if (_multiBuff == null)
            {
                _multiBuff = new Dictionary<BuffType, float>();
            }
            return _multiBuff;
        }
    }

    public void SetAddItemBuff(BuffType buffType, int value)
    {
        if (_addItemBuff.ContainsKey(buffType))
        {
            return;
        }
        _addItemBuff.Add(buffType, value);
    }

    public void SetAddBuff(BuffType buffType, int value)
    {
        if (_addBuff.ContainsKey(buffType))
        {
            return;
        }
        _addBuff.Add(buffType, value);
    }

    public void SetMultiItemBuff(BuffType buffType, float value)
    {
        if (_multiItemBuff.ContainsKey(buffType))
        {
            return;
        }
        _multiItemBuff.Add(buffType, value);
    }

    public void SetMultiBuff(BuffType buffType, float value)
    {
        if (_multiBuff.ContainsKey(buffType))
        {
            return;
        }
        _multiBuff.Add(buffType, value);
    }

    public void RemoveAddItemBuff(BuffType buffType)
    {
        if (_addItemBuff.ContainsKey(buffType))
        {
            _addItemBuff.Remove(buffType);
        }
    }

    public void RemoveAddBuff(BuffType buffType)
    {
        if (_addBuff.ContainsKey(buffType))
        {
            _addBuff.Remove(buffType);
        }
    }

    public void RemoveMultiItemBuff(BuffType buffType)
    {
        if (_multiItemBuff.ContainsKey(buffType))
        {
            _multiItemBuff.Remove(buffType);
        }
    }

    public void RemoveMultiBuff(BuffType buffType)
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

    public void ClearWeapon()
    {
        RemoveAddItemBuff(BuffType.WEAPON);
        RemoveAddBuff(BuffType.WEAPON);
        RemoveMultiItemBuff(BuffType.WEAPON);
        RemoveMultiBuff(BuffType.WEAPON);
    }

    public virtual int StatClipper()
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
        w = Clip(w);
        return (int)w;
    }

    public virtual float FloatStatClipper()
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
        v = Clip(v);
        return v;
    }

    public float Clip(float f)
    {
        return Mathf.Clamp(f, 0.0f, 65536f);
    }
}
