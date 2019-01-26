using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CTweenPosition))]
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
    CTweenPosition _pTweenPos = null;

    // -----------------------

    public void DoAttack_Melee(GameObject pObjectTarget)
    {
        Weapon pWeaponCurrent = p_pWeapon_Equiped == null ? _pWeapon_Hands : p_pWeapon_Equiped;
        _pTweenPos.DoPlayTween_Forward();
        //pWeaponCurrent.DoFireWeapon();

        var target = pObjectTarget.GetComponent<CharacterModel>();
        float damage = pStat.GetMeleeWeaponDamage(pWeaponCurrent.Damage);
        target.pStat.HP = Mathf.Clamp(CalcDamage(damage, pObjectTarget), 0f, pStat.finalHP);

        if (target.pStat.HP <= 0f)
        {
            //Dead Event
        }

        Debug.Log(name + " DoAttack_Melee pObjectTarget : " + pObjectTarget.name);
    }

    public void DoAttack_Range(GameObject pObjectTarget)
    {
        Weapon pWeaponCurrent = p_pWeapon_Equiped_Ranged == null ? _pWeapon_Hands : p_pWeapon_Equiped_Ranged;
        _pTweenPos.DoPlayTween_Forward();
        //pWeaponCurrent.DoFireWeapon();

        Debug.Log(name + " DoAttack_Range pObjectTarget : " + pObjectTarget.name);
    }

    public void GetWeapon(Weapon pWeapon)
    {
        p_pWeapon_Equiped = pWeapon;

        pStat?.SetItemStat(pWeapon, null);
        pWeapon?.DoEquipWeapon(true);
        //pStat.DoIncrease_Stat(pWeapon.effects);

        p_Event_OnChange_Weapon.DoNotify(pWeapon);
    }

    public void GetArmor(Armor pArmor)
    {
        p_pArmor_Equiped = pArmor;
        pStat?.SetItemStat(null, pArmor);
        //pStat.DoIncrease_Stat(pArmor.effects);

        p_Event_OnChange_Armor.DoNotify(pArmor);
    }

    protected override void OnAwake()
    {
        base.OnAwake();

        pStat?.DoInit();
    }

    public float CalcDamage(float hp, GameObject target)
    {
        float damage = hp;
        Vector3 pos = this.transform.position;
        float posDistance = Vector3.Distance(pos, target.transform.position);
        pos = Camera.main.WorldToScreenPoint(pos);

        if (UnityEngine.Random.Range(0f, 1f) > pStat.GetFinalHitRatio(posDistance))
        {
            FloatingTextController.CreateFloatingText("! 감 나 빗", pos);
            return 0f;
        }

        if (UnityEngine.Random.Range(0f, 1f) < pStat.GetCriticalRatio(target.GetComponent<CharacterModel>(), this))
        {
            damage *= 1.5f;
        }
        
        string indicator = damage.ToString();
        FloatingTextController.CreateFloatingText(indicator, pos);
        return damage;
    }
}
