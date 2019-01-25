using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat
{
    private string _name;
    //스탯은 정수 또는 부동소수점 값을 갖는다.
    protected int _value;
    protected float _fvalue;
    //스탯에는 증가 버프와 배수 버프가 있다.
    //증가 버프 적용 후 배수 버프가 적용된다.
    protected Dictionary<string, int> _addItemBuff;
    protected Dictionary<string, int> _addBuff;
    protected Dictionary<string, float> _multiItemBuff;
    protected Dictionary<string, float> _multiBuff;

    public Stat (string name, int value, float fvalue)
    {
        Name = name;
        Value = value;
        FValue = fvalue;
    }

    public string Name
    {
        get { return _name; }
        set { _name = value; }
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

    public Dictionary<string, int> AddItemBuff
    {
        get
        {
            if (_addItemBuff == null)
            {
                _addItemBuff = new Dictionary<string, int>();
            }
            return _addItemBuff;
        }
    }

    public Dictionary<string, int> AddBuff
    {
        get
        {
            if (_addBuff == null)
            {
                _addBuff = new Dictionary<string, int>();
            }
            return _addBuff;
        }
    }

    public Dictionary<string, float> MultiItemBuff
    {
        get
        {
            if (_multiItemBuff == null)
            {
                _multiItemBuff = new Dictionary<string, float>();
            }
            return _multiItemBuff;
        }
    }

    public Dictionary<string, float> MultiBuff
    {
        get
        {
            if (_multiBuff == null)
            {
                _multiBuff = new Dictionary<string, float>();
            }
            return _multiBuff;
        }
    }

    public void SetAddItemBuff(string buffName, int value)
    {
        if (_addItemBuff.ContainsKey(buffName))
        {
            return;
        }
        _addItemBuff.Add(buffName, value);
    }

    public void SetAddBuff(string buffName, int value)
    {
        if (_addBuff.ContainsKey(buffName))
        {
            return;
        }
        _addBuff.Add(buffName, value);
    }

    public void SetMultiItemBuff(string buffName, float value)
    {
        if (_multiItemBuff.ContainsKey(buffName))
        {
            return;
        }
        _multiItemBuff.Add(buffName, value);
    }

    public void SetMultiBuff(string buffName, float value)
    {
        if (_multiBuff.ContainsKey(buffName))
        {
            return;
        }
        _multiBuff.Add(buffName, value);
    }

    public void RemoveAddItemBuff(string buffName)
    {
        if (_addItemBuff.ContainsKey(buffName))
        {
            _addItemBuff.Remove(buffName);
        }
    }

    public void RemoveAddBuff(string buffName)
    {
        if (_addBuff.ContainsKey(buffName))
        {
            _addBuff.Remove(buffName);
        }
    }

    public void RemoveMultiItemBuff(string buffName)
    {
        if (_multiItemBuff.ContainsKey(buffName))
        {
            _multiItemBuff.Remove(buffName);
        }
    }

    public void RemoveMultiBuff(string buffName)
    {
        if (_multiBuff.ContainsKey(buffName))
        {
            _multiBuff.Remove(buffName);
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
        w = Clip(w);
        return (int)w;
    }

    public virtual float FloatStatClipper()
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
        v = Clip(v);
        return v;
    }

    public float Clip(float f)
    {
        return Mathf.Clamp(f, 0.0f, 65536f);
    }
}
