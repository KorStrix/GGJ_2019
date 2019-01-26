using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CTweenPosition))]
public class CharacterModel : CObjectBase
{
    public CObserverSubject<Weapon> p_Event_OnChange_Weapon { get; private set; } = new CObserverSubject<Weapon>();
    public CObserverSubject<Weapon> p_Event_OnChange_Weapon_Ranged { get; private set; } = new CObserverSubject<Weapon>();
    public CObserverSubject<Armor> p_Event_OnChange_Armor { get; private set; } = new CObserverSubject<Armor>();

    public Weapon _pWeapon_Equied { get; private set; }
    public Weapon _pWeapon_Equied_Ranged { get; private set; }
    public Armor _pArmor_Equiped { get; private set; }

    // -----------------------

    public Stats pStat;

    Weapon _pWeapon_Hands = null;

    [GetComponentInChildren]
    CTweenPosition _pTweenPos = null;

    // -----------------------

    public void DoAttack_Melee(GameObject pObjectTarget)
    {
        Weapon pWeaponCurrent = _pWeapon_Equied == null ? _pWeapon_Hands : _pWeapon_Equied;
        _pTweenPos.DoPlayTween_Forward();
        pWeaponCurrent.DoFireWeapon();

        Debug.Log(name + " DoAttack_Melee pObjectTarget : " + pObjectTarget.name);
    }

    public void DoAttack_Range(GameObject pObjectTarget)
    {
        Weapon pWeaponCurrent = _pWeapon_Equied_Ranged == null ? _pWeapon_Hands : _pWeapon_Equied_Ranged;
        _pTweenPos.DoPlayTween_Forward();
        pWeaponCurrent.DoFireWeapon();

        Debug.Log(name + " DoAttack_Range pObjectTarget : " + pObjectTarget.name);
    }

    public void GetWeapon(Weapon pWeapon)
    {
        _pWeapon_Equied = pWeapon;
        pStat.DoIncrease_Stat(pWeapon.effects);

        p_Event_OnChange_Weapon.DoNotify(pWeapon);
    }

    public void GetArmor(Armor pArmor)
    {
        _pArmor_Equiped = pArmor;
        pStat.DoIncrease_Stat(pArmor.effects);

        p_Event_OnChange_Armor.DoNotify(pArmor);
    }

    protected override void OnAwake()
    {
        base.OnAwake();

        pStat.DoInit();
    }
}
