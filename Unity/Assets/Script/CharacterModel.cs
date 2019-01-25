using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterModel : MonoBehaviour {

    //public delegate void CharacterDeadHandler(CharacterModel char);
    //public event CharacterDeadHandler CharacterDead;

	public struct CharacterInfo
    {
        public float speedX, speedZ;
    }

    public BasicStats basics {get; private set;} = new BasicStats();
    public SecondStats seconds {get; private set;} = new SecondStats();

    public Weapon myWeapon;
    public Armor myArmor;
    public Jewel myJewel;

    public void Awake()
    {
        PlayerItemCollector.EventOnGetWeapon += GetWeapon;
        PlayerItemCollector.EventOnGetArmor += GetArmor;
        PlayerItemCollector.EventOnGetJewel += GetJewel;
    }

    public void GetWeapon(Weapon weapon)
    {
        if (myWeapon != null)
        {
            basics.health.ClearWeapon();
            basics.strength.ClearWeapon();
            basics.dexterity.ClearWeapon();
            basics.speed.ClearWeapon();
            basics.luck.ClearWeapon();
        }
    }

    public void GetArmor(Armor armor)
    {

    }

    public virtual GetJewel(Jewel jewel)
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
