using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Stats : ScriptableObject
{
    public CObserverSubject<Stats> p_Event_OnChangeStatus { get; private set; } = new CObserverSubject<Stats>();

    public int iHP;
    public int iHP_MAX;
    public int iArmor;

    public float fDetectArea;
    public float fSpeed;

    public void DoInit()
    {
        iHP = iHP_MAX;

        p_Event_OnChangeStatus.DoNotify(this);
    }

    public void DoDamage(int iDamage)
    {
        iHP -= iDamage;

        p_Event_OnChangeStatus.DoNotify(this);
    }

    public float GetRemainHP_0_1()
    {
        return (float)iHP / iHP_MAX;
    }
}