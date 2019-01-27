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
    /// 무기의 정확도
    /// </summary>
    public float Accuracy;

    /// <summary>
    /// 무기의 사거리
    /// </summary>
    public float Range;
    
    /// <summary>
    /// 무기의 쿨타임
    /// </summary>
    public float Cooltime;
    public float Cooltime_Remain;

    /// <summary>
    /// 무기의 효과
    /// </summary>
    public StatEffects effects;


    /// <summary>
    /// 무기의 시각 효과
    /// </summary>
    public CEffect VisualEffect;

    /// <summary>
    /// 발사 시 오디오
    /// </summary>
    public AudioClip ShootClip;


    public Sprite p_pSprite_OnUI { get { return _pSprite_OnDropImage.sprite; } }

    [GetComponentInChildren("ItemSprite")]public SpriteRenderer _pSprite_OnDropImage = null;
    //public SpriteRenderer _pSprite_OnHeldSprite = null;

    public bool DoCheck_IsReadyToFire(float fDistance)
    {
        return Cooltime_Remain <= 0f && fDistance <= Range;
    }

    public void DoFire_Weapon()
    {
        Cooltime_Remain = Cooltime;

        CManagerSound.instance.DoPlaySoundEffect_OrNull(ShootClip);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (Cooltime_Remain > 0f)
            Cooltime_Remain -= Time.deltaTime;
    }
}
