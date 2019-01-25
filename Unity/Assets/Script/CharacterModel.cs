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

    public BasicStats basics;
    public SecondStats seconds;
    public StatEffects effects;

    public void Awake()
    {
        
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
