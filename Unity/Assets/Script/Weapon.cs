using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum WeaponType { knife}
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
    /// 남은 탄알 수(-1은 무한)
    /// </summary>
    public int Ammo;


}
