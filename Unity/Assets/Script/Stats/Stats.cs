using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Stats : ScriptableObject
{
    public CObserverSubject<Stats> p_Event_OnChangeStatus { get; private set; } = new CObserverSubject<Stats>();

    public float charHP;//HP;
    public float charStr;//strength;
    public float charDex;//dexterity;
    public float speed = 100f;
    public float charLuk;//luck;
    public float fDetectArea;

    SecondStats _pSecondStat;

    /* 임시 스탯 */
    private float itemStr, itemDex, itemLuk, itemHP;
    private float itemHitRatio, itemDamage, itemRange, posDistance, thiefFinalLuk, PCFinalLuk;

    public float finalStr { get { return ((charStr + itemStr) * 0.1f) + 1; } private set {} }
    public float MeleeWeaponDamage { get { return itemDamage * finalStr; } private set {} }

    public float finalDex { get { return ((charDex + itemDex) * 0.1f) + 1; } private set {} }
    public float RangeWeaponDamage { get { return itemDamage * finalDex; } private set {} }

    public float midHitRatio { get { return itemHitRatio * finalDex; } private set {} }
    public float posComp { get { return Mathf.Clamp((itemRange - posDistance) / itemRange, 0f, 1f); } private set {} }
    public float finalHitRatio { get { return midHitRatio * posComp; } private set {} }

    public float finalLuk { get { return ((charLuk + itemLuk) * 0.1f) + 1; } private set {} }
    public float gapLuk { get { return Mathf.Clamp(thiefFinalLuk - PCFinalLuk, 0f, 0.8f); } private set {} }
    public float criticalRatio { get { return 0.2f + gapLuk; } private set {} }

    public float HP { get; set; }
    public float finalHP { get { return charHP + itemHP; } private set {} }

    public void DoInit()
    {
        if (_pSecondStat == null)
            _pSecondStat = new SecondStats();

        p_Event_OnChangeStatus.DoNotify(this);
    }

    public SecondStats GetSecondStat(Weapon pCurrentWeapon, Armor pCurrentArmor)
    {
        _pSecondStat.CurrentHP = HP;
        _pSecondStat.CurrentHP_MAX = finalHP;
        _pSecondStat.damage = finalStr;
        _pSecondStat.defence = finalDex;
        _pSecondStat.accuracy = finalHitRatio;
        _pSecondStat.crit = finalLuk;
        _pSecondStat.evasion = finalLuk;

        return _pSecondStat;
    }

    public void DoIncrease_Stat(StatEffects pStatEffect)
    {
        if (pStatEffect == null)
            return;
        /*
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
        */

        p_Event_OnChangeStatus.DoNotify(this);
    }

    public float GetMeleeWeaponDamage(float _itemDamage)
    {
        itemDamage = _itemDamage;
        return MeleeWeaponDamage;
    }

    public float GetRangeWeaponDamage(float _charDex, float _itemDex, float _itemDamage)
    {
        charDex = _charDex;
        itemDex = _itemDex;
        itemDamage = _itemDamage;
        return RangeWeaponDamage;
    }

    public float GetFinalHitRatio(float _posDistance)
    {
        posDistance = _posDistance;
        return finalHitRatio;
    }

    public float GetCriticalRatio(CharacterModel target, CharacterModel attacker)
    {
        float temp1 = charLuk;
        float temp2 = itemLuk;

        charLuk = target.pStat.charLuk;
        itemLuk = target.pStat.itemLuk;
        thiefFinalLuk = finalLuk;

        charLuk = attacker.pStat.charLuk;
        itemLuk = attacker.pStat.itemLuk;
        PCFinalLuk = finalLuk;

        float temp3 = criticalRatio;
        charLuk = temp1;
        itemLuk = temp2;

        return temp3;
    }

    public float GetFinalHP(float _charHP, float _itemHP)
    {
        charHP = _charHP;
        itemHP = _itemHP;
        return finalHP;
    }

    public void SetItemStat(Weapon weapon = null, Armor armor = null)
    {
        itemStr = 0;
        itemDex = 0;
        itemLuk = 0;
        itemHP = 0;
        itemHitRatio = 0;
        itemRange = 0;

        if (weapon != null)
        {
            itemRange = weapon.Range;
            foreach(DeltaStat d in weapon.effects.deltaStats)
            {
                switch(d.type)
                {
                    case StatType.STR:
                        itemStr += d.value;
                        break;
                    case StatType.DEX:
                        itemDex += d.value;
                        break;
                    case StatType.LUCK:
                        itemLuk += d.value;
                        break;
                    case StatType.HP:
                        itemHP += d.value;
                        break;
                    case StatType.ACCURACY:
                        itemHitRatio += d.value;
                        break;
                    default:
                        //exception!
                        break;
                }
            }
        }

        if (armor == null)
        {
            return;
        }
        foreach(DeltaStat d in armor.effects.deltaStats)
        {
            switch(d.type)
            {
                case StatType.STR:
                    itemStr += d.value;
                    break;
                case StatType.DEX:
                    itemDex += d.value;
                    break;
                case StatType.LUCK:
                    itemLuk += d.value;
                    break;
                case StatType.HP:
                    itemHP += d.value;
                    break;
                default:
                    //exception!
                    break;
            }
        }
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