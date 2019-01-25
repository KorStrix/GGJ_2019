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
    private void OnTriggerEnter(Collider other) {
        switch (other.tag) {


        }
        var h = other.GetComponent<Holdable>();
        if (h != null) {
            h.Attach(transform);
            if (weaponHeld != null)
                weaponHeld.GetComponent<Holdable>().Detach();
            weaponHeld = h.GetComponent<Weapon>();
        }
    }
}
