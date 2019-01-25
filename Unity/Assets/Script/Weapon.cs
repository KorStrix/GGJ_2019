using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum WeaponType { fist, melee, handgun, rifle}
public class Weapon : MonoBehaviour {
    /// <summary>
    /// 무기의 이름
    /// </summary>
    public string Name;


    /// <summary>
    /// 무기 타입
    /// </summary>
    public WeaponType Type;

    /// <summary>
    /// 무기가 입히는 피해
    /// </summary>
    public float Damage;

    /// <summary>
    /// 무기의 사거리
    /// </summary>
    public float Range;

    /// <summary>
    /// 무기의 쿨타임
    /// </summary>
    public float Cooltime;

    /// <summary>
    /// 무기의 명중률
    /// </summary>
    public float Accuracy;


}
