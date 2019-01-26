using System.Collections;
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
    private List<Jewel> _listJewel = new List<Jewel>();

    public void DoAttack()
    {
        //CManagerEffect.instance.DoPlayEffect(_pWeaponEquied.VisualEffect);
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
    
    public void GetJewel(Jewel jewel)
    {
        _listJewel.Add(jewel);
    }

    protected override void OnAwake()
    {
        base.OnAwake();

       
    }
}
