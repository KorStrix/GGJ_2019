using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    /// <summary>
    /// 무기의 이름
    /// </summary>
    public string Name;

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

    bool isHeld = false;

    /// <summary>
    /// 무기 오브젝트를 캐릭터에 부착합니다. 무기 아이템 스프라이트가 보이지 않게 됩니다.
    /// </summary>
    /// <param name="character"></param>
    public void Attach(Transform character) {
        transform.parent = character;
        transform.localPosition = Vector3.zero;
        GetComponent<Renderer>().SetActive(false);
        isHeld = true;
    }

    /// <summary>
    /// 무기 오브젝트를 바닥에 내려놓습니다. 무기 아이템 스프라이트가 보입니다.
    /// </summary>
    public void Detach() {
        transform.SetParent(null, true);
        isHeld = false;
        GetComponent<Renderer>().SetActive(true);
    }
}
