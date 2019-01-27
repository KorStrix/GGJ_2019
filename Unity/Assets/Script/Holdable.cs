using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holdable : CObjectBase {

    public delegate void OnAttach(Transform collector);
    public delegate void OnDetach();

    public event OnAttach EventOnAttach;
    public event OnDetach EventOnDetach;


    [SerializeField] float nonInteractTime = 1f;
    const float SPEEDSCALE = 3f;

    [GetComponentInChildren("mask")] Renderer Itemform;
    [GetComponentInChildren("ItemSprite")] Renderer Itemform2;
    [GetComponentInChildren("HeldSprite")] Renderer Heldform;

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
        EventOnAttach?.Invoke(character);
        Debug.Log("Attach Weapon");
        transform.parent = character;
        transform.localPosition = Vector3.zero;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        Itemform.enabled = false;
        Itemform2.enabled = false;
        Heldform.enabled = true;
        foreach (var c in GetComponents<Collider>())c.enabled = false;
        isHeld = true;
    }

    /// <summary>
    /// 이 오브젝트를 바닥에 내려놓습니다. 무기 아이템 스프라이트가 보입니다.
    /// </summary>
    public void Detach(Vector3 speed) {
        EventOnDetach?.Invoke();
        Debug.Log("Detach!");
        transform.SetParent(null, true);
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        GetComponent<Rigidbody>().velocity = speed*SPEEDSCALE;
        isHeld = false;
        Itemform.enabled = true;
        Itemform2.enabled = true;
        Heldform.enabled = false;

        StartCoroutine(WaitAfterDetached());
        
    }

    public IEnumerator WaitAfterDetached() {
        yield return new WaitForSeconds(nonInteractTime);

        foreach (var c in GetComponents<Collider>()) c.enabled = true;

    }
    
}
