using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemCollector : MonoBehaviour {


    public delegate void OnGetWeapon(Weapon newWeapon);
    public delegate void OnGetArmor(Armor newArmor);
    public delegate void OnGetJewel(Jewel newJewel);
    public delegate void OnGetConsumable(Consumable consumable);

    public event OnGetWeapon EventOnGetWeapon;
    public event OnGetArmor EventOnGetArmor;
    public event OnGetJewel EventOnGetJewel;
    public event OnGetConsumable EventOnGetConsumable;

    public Weapon WeaponHeld { get; private set; }
    public Armor ArmorHeld { get; private set; }
    public Jewel JewelHeld { get; private set; }

    Weapon fist;
    // Use this for initialization
    void Start () {
        fist = GetComponentInChildren<Weapon>();

        //Search for Character
        var ch = GetComponentInParent<CharacterModel>();
        if (ch != null)
        {
            EventOnGetWeapon += ch.GetWeapon;
            EventOnGetArmor += ch.GetArmor;
            EventOnGetJewel += ch.GetJewel;
        }
    }


    // Update is called once per frame
    void Update () {
		
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
                WeaponHeld.GetComponent<Holdable>().Detach(transform.forward);
            WeaponHeld = h.GetComponent<Weapon>();
            EventOnGetWeapon(WeaponHeld);
            break;
        case "Armor":
            var h2 = other.GetComponent<Holdable>();
            h2.Attach(transform);
            if (ArmorHeld != null)
                ArmorHeld.GetComponent<Holdable>().Detach(transform.forward);
            ArmorHeld = h2.GetComponent<Armor>();
            EventOnGetArmor(ArmorHeld);
            break;
        case "Consumable":
            Debug.Log("consume!");
            var c = other.GetComponent<Consumable>();
            EventOnGetConsumable?.Invoke(c);
            Destroy(other.gameObject);
            break;
        case "Jewel":
            var h3 = other.GetComponent<Holdable>();
            h3.Attach(transform);
            if (JewelHeld != null)
                JewelHeld.GetComponent<Holdable>().Detach(transform.forward);
            JewelHeld = h3.GetComponent<Jewel>();
            EventOnGetJewel(JewelHeld);
            break;
        }
        
           
        

        
    }
    public void DropWeapon() {
        if (WeaponHeld == fist) return;
        else {
            WeaponHeld.GetComponent<Holdable>().Detach(transform.forward);
            WeaponHeld = fist;
        }
    }
    public void DropArmor() {
        ArmorHeld.GetComponent<Holdable>().Detach(transform.forward);
        ArmorHeld = null;

    }
    public void DropJewel() {
        JewelHeld.GetComponent<Holdable>().Detach(transform.forward);
        JewelHeld = null;
    }
}
