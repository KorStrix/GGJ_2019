using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ArmorType { }
public class Armor : MonoBehaviour {

    /// <summary>
    /// 방어구의 이름
    /// </summary>
    public string Name;


    /// <summary>
    /// 방어구 타입
    /// </summary>
    public ArmorType Type;

    public StatEffects effects;


}
