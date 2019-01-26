using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum WeaponType { fist, melee, handgun, rifle}
public class Weapon : CObjectBase {
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

    public float fAttackDelay;

    /// <summary>
    /// 무기의 효과
    /// </summary>
    public StatEffects effects;


    /// <summary>
    /// 무기의 시각 효과
    /// </summary>
    public CEffect VisualEffect;

    public bool DoCheck_IsReadyToFire()
    {
        return Cooltime < 0f;
    }

    public void DoFireWeapon()
    {
        if(VisualEffect != null)
            CManagerEffect.instance.DoPlayEffect(VisualEffect.ToString(), transform.position);

        Cooltime = fAttackDelay;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (Cooltime > 0f)
            Cooltime -= Time.deltaTime;
    }

}
