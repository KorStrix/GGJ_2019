using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class StatEffects : ScriptableObject {
    public enum EffectType {
        Weapon,Armor,Consumable

    }
    public EffectType Type;
    public List<DeltaStat> deltaStats;
}
