using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicStats : StatCollection
{
    public Stat health, strength, dexterity, speed, luck;

    public BasicStats()
    {
        ConfigureStats();
    }

    protected override void ConfigureStats()
    {
        health = CreateStat(StatType.BASEHP, 100, 100.0f);
        strength = CreateStat(StatType.STR, 100, 100.0f);
        dexterity = CreateStat(StatType.DEX, 100, 100.0f);
        speed = CreateStat(StatType.SPEED, 100, 100.0f);
        luck = CreateStat(StatType.LUCK, 100, 100.0f);
    }
}