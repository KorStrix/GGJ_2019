using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicStats : StatCollection
{
    public Stat health;

    public BasicStats()
    {
        ConfigureStats();
    }

    protected override void ConfigureStats()
    {
        health = CreateMutableStat("Health", 100, 100.0f);        
    }
}