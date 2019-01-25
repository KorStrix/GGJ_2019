using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondStats : StatCollection
{
    public Stat hp, damage, defence, accuracy, crit, evasion;

    public void BasicStats()
    {
        ConfigureStats();
    }

    protected override void ConfigureStats()
    {
        hp = CreateMutableStat(StatType.HP, 100, 100.0f);
        damage = CreateStat(StatType.DAMAGE, 100, 100.0f);
        defence = CreateStat(StatType.DEFENCE, 100, 100.0f);
        accuracy = CreateStat(StatType.ACCURACY, 100, 100.0f);
        crit = CreateStat(StatType.CRITCHANCE, 10, 10.0f);
        evasion = CreateStat(StatType.EVASION, 10, 10.0f);
    }
}