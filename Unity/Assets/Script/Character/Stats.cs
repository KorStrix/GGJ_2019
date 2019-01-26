using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Stats : ScriptableObject
{
    public CObserverSubject<Stats> p_Event_OnChangeStatus { get; private set; } = new CObserverSubject<Stats>();

    public float HP;
    public float strength;
    public float dexterity;
    public float speed;
    public float luck;
    public float fDetectArea;

    SecondStats _pSecondStat;

    public void DoInit()
    {
        if (_pSecondStat == null)
            _pSecondStat = new SecondStats();

        p_Event_OnChangeStatus.DoNotify(this);
    }

    public SecondStats GetSecondStat(Weapon pCurrentWeapon, Armor pCurrentArmor)
    {
        _pSecondStat.CurrentHP = HP;
        _pSecondStat.CurrentHP_MAX = HP;
        _pSecondStat.damage = strength;
        _pSecondStat.defence = dexterity;
        _pSecondStat.accuracy = dexterity;
        _pSecondStat.crit = speed;
        _pSecondStat.evasion = luck;

        return _pSecondStat;
    }

    public void DoIncrease_Stat(StatEffects pStatEffect)
    {
        if (pStatEffect == null)
            return;

        foreach (DeltaStat d in pStatEffect.deltaStats)
        {
            switch (d.type)
            {
                case StatType.BASEHP:
                    HP += d.value;
                    break;

                case StatType.STR:
                    strength += d.value;
                    break;

                case StatType.DEX:
                    dexterity += d.value;
                    break;

                case StatType.SPEED:
                    speed += d.value;
                    break;

                case StatType.LUCK:
                    luck += d.value;
                    break;

                default:
                    //Exception!
                    break;
            }
        }

        p_Event_OnChangeStatus.DoNotify(this);
    }
}

public class SecondStats
{
    public float CurrentHP;
    public float CurrentHP_MAX;

    public float damage;
    public float defence;
    public float accuracy;
    public float crit;
    public float evasion;
}