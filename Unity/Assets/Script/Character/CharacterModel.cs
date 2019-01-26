using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ECharacterAnimationName
{
    Character_OnAttack,
    Character_OnHit,
}

public class CharacterModel : CObjectBase
{
    public CObserverSubject<Weapon> p_Event_OnChange_Weapon { get; private set; } = new CObserverSubject<Weapon>();
    public CObserverSubject<Weapon> p_Event_OnChange_Weapon_Ranged { get; private set; } = new CObserverSubject<Weapon>();
    public CObserverSubject<Armor> p_Event_OnChange_Armor { get; private set; } = new CObserverSubject<Armor>();

    public Weapon p_pWeapon_Equiped { get; private set; }
    public Weapon p_pWeapon_Equiped_Ranged { get; private set; }
    public Armor p_pArmor_Equiped { get; private set; }

    // -----------------------

    public Stats pStat;

    Weapon _pWeapon_Hands = null;
    Armor _pArmor_Torso = null;

    [GetComponentInChildren]
    CAnimatorController _pAnimator = null;

    // -----------------------

    public void DoAttack_Melee(GameObject pObjectTarget)
    {
        Weapon pWeaponCurrent = p_pWeapon_Equiped == null ? _pWeapon_Hands : p_pWeapon_Equiped;
        pWeaponCurrent.DoFireWeapon();
        _pAnimator.DoPlayAnimation(ECharacterAnimationName.Character_OnAttack);
        //pWeaponCurrent.DoFireWeapon();

        var target = pObjectTarget.GetComponent<CharacterModel>();
        if (target == null)
            return;

        int damage = (int)pWeaponCurrent.Damage;
        target.pStat.iHP -= damage;

        target._pAnimator.DoPlayAnimation(ECharacterAnimationName.Character_OnHit);
        target.SendMessage(nameof(IResourceEventListener.IResourceEventListener_Excute), "OnHit");
        if (target.pStat.iHP <= 0f)
        {
            //Dead Event
        }
    }

    public void DoAttack_Range(GameObject pObjectTarget)
    {
        Weapon pWeaponCurrent = p_pWeapon_Equiped_Ranged == null ? _pWeapon_Hands : p_pWeapon_Equiped_Ranged;
        _pAnimator.DoPlayAnimation("Character_OnAttack");
        //pWeaponCurrent.DoFireWeapon();

        Debug.Log(name + " DoAttack_Range pObjectTarget : " + pObjectTarget.name);
    }

    public void GetWeapon(Weapon pWeapon)
    {
        p_pWeapon_Equiped = pWeapon;

        pWeapon.DoEquipWeapon(true);
        //pStat.DoIncrease_Stat(pWeapon.effects);

        p_Event_OnChange_Weapon.DoNotify(pWeapon);
    }

    public void GetArmor(Armor pArmor)
    {
        p_pArmor_Equiped = pArmor;
        //pStat.DoIncrease_Stat(pArmor.effects);

        p_Event_OnChange_Armor.DoNotify(pArmor);
    }

    protected override void OnAwake()
    {
        base.OnAwake();

        pStat?.DoInit();
    }
}
