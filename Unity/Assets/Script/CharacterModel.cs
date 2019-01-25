using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterModel : MonoBehaviour {

	public struct CharacterInfo
    {
        public float speedX, speedZ;
    }

    public BasicStats basics = new BasicStats();
    public SecondStats seconds = new SecondStats();

    public int myHealth = 100;
    public int myStrength = 100;
    public int myDexterity = 100;
    public int mySpeed = 10;
    public int myLuck = 10;

    private Weapon myWeapon;
    private Armor myArmor;
    private Jewel myJewel;

    public void Awake()
    {
        StatUpdate();
    }

    public void Update()
    {
        StatUpdate();
    }

    public void StatUpdate()
    {
        basics.health.Value = myHealth;
        basics.strength.Value = myStrength;
        basics.dexterity.Value = myDexterity;
        basics.speed.Value = mySpeed;
        basics.luck.Value = myLuck;
    }

    public void GetWeapon(Weapon weapon)
    {
        myWeapon = weapon;
    }

    public void GetArmor(Armor armor)
    {
        if (myArmor != null)
        {
            basics.health.ClearArmor();
            basics.strength.ClearArmor();
            basics.dexterity.ClearArmor();
            basics.speed.ClearArmor();
            basics.luck.ClearArmor();
        }
        myArmor = armor;
        foreach (DeltaStat d in armor.effects.deltaStats)
        {
            switch (d.type)
            {
                case StatType.BASEHP:
                    basics.health.SetAddItemBuff(BuffType.ARMOR, d.value);
                    break;
                case StatType.STR:
                    basics.strength.SetAddItemBuff(BuffType.ARMOR, d.value);
                    break;
                case StatType.DEX:
                    basics.dexterity.SetAddItemBuff(BuffType.ARMOR, d.value);
                    break;
                case StatType.SPEED:
                    basics.speed.SetAddItemBuff(BuffType.ARMOR, d.value);
                    break;
                case StatType.LUCK:
                    basics.luck.SetAddItemBuff(BuffType.ARMOR, d.value);
                    break;
                default:
                    //Exception!
                    break;
            }
        }
    }
    
    public virtual void GetJewel(Jewel jewel)
    {
        //
    }

    public void Attack(CharacterModel ch)
    {
        //
    }
    
    public void StatsUpdate()
    {
        /*
        //
        foreach (DeltaStat d in effects.deltaStat)
        {
            //effect 종류 모름
        }
        SecondStatsUpdate();
        */
    }

    public void SecondStatsUpdate()
    {
        //
    }

}
