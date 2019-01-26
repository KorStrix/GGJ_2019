using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ArmorType {Head, Torso }
public class Armor : MonoBehaviour {

    /// <summary>
    /// 방어구의 이름
    /// </summary>
    public string Name;

    /// <summary>
    /// UI용 스프라이트
    /// </summary>
    public Sprite p_pSprite_OnUI { get { return _pSprite_OnDropImage.sprite; } }

    [GetComponentInChildren("ItemSprite")] public SpriteRenderer _pSprite_OnDropImage = null;
    /// <summary>
    /// 방어구 타입
    /// </summary>
    public ArmorType Type;

    public StatEffects effects;


}
