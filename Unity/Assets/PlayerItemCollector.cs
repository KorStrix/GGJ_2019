using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemCollector : MonoBehaviour {
    Weapon weaponHeld;
    Armor armorHeld;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// 아이템을 이곳에서 줍습니다.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other) {

        Debug.Log("TODO : 아이템을 줍는 코드 삽입");
        switch (other.tag) {


        }
        var h = other.GetComponent<Holdable>();
        if (h != null) {
            h.Attach(transform);
            if (weaponHeld != null)
                weaponHeld.GetComponent<Holdable>().Detach();
            weaponHeld = h.GetComponent<Weapon>();
        }

        var c = other.GetComponent<Consumable>();
        if (c != null) {
            var e = c.Effects;
            //stat read
            //GetComponentInParent<CharacterModel>().basics.

        }
    }
}
