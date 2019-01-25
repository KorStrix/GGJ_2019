using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicStats
{
    public Stat health, strength, dexterity, speed, luck;
    public StatCollection myCollection = new StatCollection();

    public BasicStats()
    {
        ConfigureStats();
    }

    public void ConfigureStats()
    {
        health = myCollection.CreateStat(StatType.BASEHP, 100, 100.0f);
        strength = myCollection.CreateStat(StatType.STR, 100, 100.0f);
        dexterity = myCollection.CreateStat(StatType.DEX, 100, 100.0f);
        speed = myCollection.CreateStat(StatType.SPEED, 100, 100.0f);
        luck = myCollection.CreateStat(StatType.LUCK, 100, 100.0f);
    }
}