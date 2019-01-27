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

    public CharacterModel p_pCharacterModel_Owner { get; private set; }

    Armor pArmor;

    public void DoInit(CharacterModel pCharacterModel)
    {
        p_pCharacterModel_Owner = pCharacterModel;
        iHP = iHP_MAX;

        pCharacterModel.p_Event_OnChange_Armor.Subscribe += P_Event_OnChange_Armor_Subscribe;

        p_Event_OnChangeStatus.DoNotify(this);
    }

    private void P_Event_OnChange_Armor_Subscribe(Armor pArmor)
    {
        this.pArmor = pArmor;
    }

    public void DoDamage(int iDamage)
    {
        int iArmorCalculated = iArmor;

        if(pArmor != null)
        {
            foreach (var pStats in pArmor.effects.deltaStats)
            {
                if (pStats.type == HomeKeeperBuffType.Armor)
                    iArmorCalculated += pStats.value;
            }
        }

        if (iDamage > iArmorCalculated)
            iHP -= Mathf.Abs(iArmorCalculated - iDamage);
        else
            iHP -= 1;

        p_Event_OnChangeStatus.DoNotify(this);
    }

    public void DoRecovory(int iRecoveryAmount)
    {
        iHP += iRecoveryAmount;
        if (iHP > iHP_MAX)
            iHP = iHP_MAX;

        p_Event_OnChangeStatus.DoNotify(this);
    }


    public float GetRemainHP_0_1()
    {
        return (float)iHP / iHP_MAX;
    }
}