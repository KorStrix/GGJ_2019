﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemCollector : MonoBehaviour {


    public delegate void OnGetWeapon(Weapon newWeapon);
    public delegate void OnGetArmor(Armor newArmor);
    //public delegate void OnGetJewel();
    public delegate void OnGetConsumable(Consumable consumable);

    public event OnGetWeapon EventOnGetWeapon;
    public event OnGetArmor EventOnGetArmor;
    //public event OnGetJewel EventOnGetJewel;
    public event OnGetConsumable EventOnGetConsumable;

    public Weapon WeaponHeld;
    public Armor ArmorHeld;
    //public Jewel JewelHeld;

    Rigidbody characterRigid;
    Weapon fist;

    public Weapon DoEquipWeapon(string strResourceName)
    {
        CManagerPooling_InResources<string, Weapon>.instance.p_strResourcesPath = "Weapon/";
        Weapon pWeapon = CManagerPooling_InResources<string, Weapon>.instance.DoPop(strResourceName);
        pWeapon.transform.SetParent(transform);
        pWeapon.transform.localPosition = Vector3.zero;

        CharacterModel pCharacterModel = GetComponentInParent<CharacterModel>();
        EventOnGetWeapon += pCharacterModel.GetWeapon;
        EventOnGetArmor += pCharacterModel.GetArmor;

        return pWeapon;
    }

    void Start()
    {
        characterRigid = GetComponentInParent<Rigidbody>();
        WeaponHeld = fist;
        WeaponHeld = fist = DoEquipWeapon("Fist");
    }

    /// <summary>
    /// 아이템을 이곳에서 줍습니다.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other) {

        switch (other.tag) {
        case "Weapon":
            var h = other.GetComponent<Holdable>();
            h.Attach(transform);
            if (WeaponHeld != fist)
                WeaponHeld.GetComponent<Holdable>().Detach(characterRigid.velocity);
            WeaponHeld = h.GetComponent<Weapon>();
            EventOnGetWeapon?.Invoke(WeaponHeld);
            break;
        case "Armor":
            var h2 = other.GetComponent<Holdable>();
            h2.Attach(transform);
            if (ArmorHeld != null)
                ArmorHeld.GetComponent<Holdable>().Detach(characterRigid.velocity);
            ArmorHeld = h2.GetComponent<Armor>();
            EventOnGetArmor?.Invoke(ArmorHeld);
            break;
        case "Consumable":
            Debug.Log("consume!");
            var c = other.GetComponent<Consumable>();
            EventOnGetConsumable?.Invoke(c);
            Destroy(other.gameObject);
            break;
        case "Jewel":

            HomeKeeperGameManager.instance.DoLose_Jewel(other.GetComponentInChildren<Jewel>());
            Destroy(other.gameObject);
            break;
        }
        
           
        

        
    }
    public void DropWeapon() {
        if (WeaponHeld == fist) return;
        else {
            WeaponHeld.GetComponent<Holdable>().Detach(characterRigid.velocity);
            WeaponHeld = fist;
            EventOnGetWeapon(fist);
        }
    }
    public void DropArmor() {
        ArmorHeld.GetComponent<Holdable>().Detach(characterRigid.velocity);
        ArmorHeld = null;
        EventOnGetArmor(null);
    }
    /*
    public void DropJewel() {
        JewelHeld.GetComponent<Holdable>().Detach(characterRigid.velocity);
        JewelHeld = null;
        EventOnGetJewel(null);
    }
    */
}
