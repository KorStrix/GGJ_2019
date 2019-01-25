using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondStats : StatCollection
{
    public Stat hp, damage, defence, accuracy, crit, evasion;
    public StatCollection myCollection = new StatCollection();

    public SecondStats()
    {
        ConfigureStats();
    }

    public void ConfigureStats()
    {
        hp = myCollection.CreateMutableStat(StatType.HP, 100, 100.0f);
        damage = myCollection.CreateStat(StatType.DAMAGE, 100, 100.0f);
        defence = myCollection.CreateStat(StatType.DEFENCE, 100, 100.0f);
        accuracy = myCollection.CreateStat(StatType.ACCURACY, 100, 100.0f);
        crit = myCollection.CreateStat(StatType.CRITCHANCE, 10, 10.0f);
        evasion = myCollection.CreateStat(StatType.EVASION, 10, 10.0f);
    }
}