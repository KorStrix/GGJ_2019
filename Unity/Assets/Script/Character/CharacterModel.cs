using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ECharacterAnimationName
{
    Character_OnAttack,
    Character_OnHit,
    Character_OnMove
}

public class CharacterModel : CObjectBase
{
    public CObserverSubject<Weapon> p_Event_OnChange_Weapon { get; private set; } = new CObserverSubject<Weapon>();
    public CObserverSubject<Armor> p_Event_OnChange_Armor { get; private set; } = new CObserverSubject<Armor>();

    public CObserverSubject<GameObject> p_Event_OnSetTarget { get; private set; } = new CObserverSubject<GameObject>();

    public Armor p_pArmor_Equiped { get; private set; }

    // -----------------------

    public Stats p_pStat_Instance { get; private set; }

    public LayerMask pTerrainLayer;

    [SerializeField]
    private Stats pStat;

    Weapon _pWeapon_Equiped;
    Weapon _pWeapon_Fist;
    Armor _pArmor_Torso = null;

    [GetComponentInChildren]
    CAnimatorController p_pAnimator = null;
    [GetComponentInChildren]
    CPhysicsTrigger _pPhysicsTrigger = null;

    // -----------------------

    public void DoAttack_Melee(GameObject pObjectTarget)
    {
        Weapon pWeaponCurrent = GetCurrentWeapon();
        pWeaponCurrent.DoFire_Weapon();
        p_pAnimator.DoPlayAnimation(ECharacterAnimationName.Character_OnAttack);

        var target = pObjectTarget.GetComponent<CharacterModel>();
        if (target == null)
            return;

        target.pStat.DoDamage((int)pWeaponCurrent.Damage);
        target.p_pAnimator.DoPlayAnimation(ECharacterAnimationName.Character_OnHit);
        target.SendMessage(nameof(IResourceEventListener.IResourceEventListener_Excute), "OnHit");
    }

    public Weapon GetCurrentWeapon()
    {
        if(_pWeapon_Fist == null && _pWeapon_Equiped == null)
            Debug.LogError(name + "GetCurrentWeapon() == null", this);

        return _pWeapon_Equiped == null ? _pWeapon_Fist : _pWeapon_Equiped;
    }

    public void GetWeapon(Weapon pWeapon)
    {
        _pWeapon_Equiped = pWeapon;
        p_Event_OnChange_Weapon.DoNotify(pWeapon);
    }

    public void GetArmor(Armor pArmor)
    {
        p_pArmor_Equiped = pArmor;
        p_Event_OnChange_Armor.DoNotify(pArmor);
    }


    public void DoStartAI()
    {
        StopCoroutine(nameof(CoAILogic));
        StartCoroutine(nameof(CoAILogic));
    }


    // ====================================================================

    protected override void OnAwake()
    {
        base.OnAwake();

        p_pStat_Instance = pStat.DoInit();
    }

    // ====================================================================

    IEnumerator CoAILogic()
    {
        yield return null;

        while (true)
        {
            yield return StartCoroutine(CoAIState_OnScanTarget());

            yield return null;

            yield return StartCoroutine(CoAIState_OnChase_ForAttack());
        }
    }

    IEnumerator CoAIState_OnScanTarget()
    {
        _pPhysicsTrigger.GetComponent<SphereCollider>().radius = pStat.fDetectArea;
        _pPhysicsTrigger.DoClear_InColliderList();

        while (_pPhysicsTrigger.GetColliderList_3D_Stay().Count == 0)
        {
            yield return null;
        }

        List<Collider> listCollider = _pPhysicsTrigger.GetColliderList_3D_Enter();
        p_Event_OnSetTarget.DoNotify(listCollider[0].gameObject);
    }


    public float fDistance_ForDebug;

    IEnumerator CoAIState_OnChase_ForAttack()
    {
        while (true)
        {
            while (_pPhysicsTrigger.GetColliderList_3D_Enter().Count == 0)
            {
                yield return null;
            }

            Transform pTransformTarget = _pPhysicsTrigger.GetColliderList_3D_Enter()[0].transform;
            fDistance_ForDebug = Vector3.Distance(transform.position, pTransformTarget.position);
            if (GetCurrentWeapon().DoCheck_IsReadyToFire(fDistance_ForDebug) &&
                Physics.Raycast(transform.position, pTransformTarget.position, pTerrainLayer) == false)
            {
                List<Collider> listCollider = _pPhysicsTrigger.GetColliderList_3D_Enter();
                for (int i = 0; i < listCollider.Count; i++)
                    DoAttack_Melee(listCollider[i].gameObject);
                break;
            }

            yield return null;
        }
    }

}
