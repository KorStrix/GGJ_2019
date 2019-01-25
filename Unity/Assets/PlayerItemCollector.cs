using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemCollector : MonoBehaviour {
    Weapon w;
    Armor a;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other) {
        var h = other.GetComponent<Holdable>();
        if (h != null) {
            h.Attach(transform);
            if (w != null)
                w.GetComponent<Holdable>().Detach();
            w = h.GetComponent<Weapon>();
        }
    }
}
