﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterModel : CObjectBase
{
    public CObserverSubject<Weapon> p_Event_OnChange_Weapon { get; private set; } = new CObserverSubject<Weapon>();
    public CObserverSubject<Armor> p_Event_OnChange_Armor { get; private set; } = new CObserverSubject<Armor>();

    // -----------------------

    public Stats pStat;

    // -----------------------

    Weapon _pWeapon_Hands = null;

    private Weapon _pWeaponEquied;
    private Armor _pArmor_Equiped;

    public void DoAttack_Melee()
    {
        Weapon pWeaponCurrent = _pWeaponEquied == null ? _pWeapon_Hands : _pWeaponEquied;
        CManagerEffect.instance.DoPlayEffect(pWeaponCurrent.VisualEffect.ToString(), transform.position);

        Debug.Log("DoAttack_Melee");
    }

    public void DoAttack_Gun()
    {
        Weapon pWeaponCurrent = _pWeaponEquied == null ? _pWeapon_Hands : _pWeaponEquied;
        CManagerEffect.instance.DoPlayEffect(pWeaponCurrent.VisualEffect.ToString(), transform.position);

        Debug.Log("DoAttack_Gun");
    }

    public void GetWeapon(Weapon weapon)
    {
        _pWeaponEquied = weapon;
    }

    public void GetArmor(Armor armor)
    {
        _pArmor_Equiped = armor;
        pStat.DoIncrease_Stat(armor.effects);
    }
}
