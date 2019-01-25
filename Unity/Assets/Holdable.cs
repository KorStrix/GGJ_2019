using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holdable : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    bool isHeld = false;

    /// <summary>
    /// 이 오브젝트를 캐릭터에 부착합니다. 무기 아이템 스프라이트가 보이지 않게 됩니다.
    /// </summary>
    /// <param name="character"></param>
    public void Attach(Transform character) {
        transform.parent = character;
        transform.localPosition = Vector3.zero;
        GetComponent<Renderer>().SetActive(false);
        isHeld = true;
    }

    /// <summary>
    /// 이 오브젝트를 바닥에 내려놓습니다. 무기 아이템 스프라이트가 보입니다.
    /// </summary>
    public void Detach() {
        transform.SetParent(null, true);
        isHeld = false;
        GetComponent<Renderer>().SetActive(true);
    }
}
