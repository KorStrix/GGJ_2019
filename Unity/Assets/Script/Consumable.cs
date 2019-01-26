using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ConsumableType { none = -1, aidKit, handgunammo, rifleammo, bandage }
public class Consumable : MonoBehaviour {
    public ConsumableType Type;
    public StatEffects Effects;

    /// <summary>
    /// 소모되었을 때에 재생되는 소리
    /// </summary>
    public AudioClip UseClip;
    private void Start() {
        
    }

}
