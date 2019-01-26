using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterAttackButton : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
        GetComponent<Button>().onClick.AddListener(() => {
            HomeKeeperGameManager.instance.Event_OnClickPlayerButton();
            

        });
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
