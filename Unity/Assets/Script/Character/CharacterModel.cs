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
    public CObserverSubject<Weapon> p_Event_OnChange_Weapon_Ranged { get; private set; } = new CObserverSubject<Weapon>();
    public CObserverSubject<Armor> p_Event_OnChange_Armor { get; private set; } = new CObserverSubject<Armor>();

    public CObserverSubject<GameObject> p_Event_OnSetTarget { get; private set; } = new CObserverSubject<GameObject>();

    public Armor p_pArmor_Equiped { get; private set; }

    [GetComponentInChildren("Model")]
    public SpriteRenderer p_pSprite_Renderer_CharacterModel { get; private set; }

    // -----------------------

    public Stats pStat;
    public LayerMask pTargetLayer;

    public Sprite p_pSprite_OnDead;

    // -----------------------

    Weapon _pWeapon_Equiped;
    Weapon _pWeapon_Fist;
    Armor _pArmor_Torso = null;

    [GetComponentInChildren]
    PlayerItemCollector _pCollector = null;

    [GetComponentInChildren]
    CAnimatorController p_pAnimator = null;
    [GetComponentInChildren]
    CPhysicsTrigger _pPhysicsTrigger = null;

    // -----------------------

    public void DoAttack_Melee(GameObject pObjectTarget)
    {
        Weapon pWeaponCurrent = GetCurrentWeapon();
        pWeaponCurrent.DoFire_Weapon((pObjectTarget.transform.position - transform.position).normalized);
        p_pAnimator.DoPlayAnimation(ECharacterAnimationName.Character_OnAttack);

        var target = pObjectTarget.GetComponent<CharacterModel>();
        if (target == null)
            return;

        target.pStat.DoDamage((int)pWeaponCurrent.Damage);
        target.p_pAnimator.DoPlayAnimation(ECharacterAnimationName.Character_OnHit);
        target.SendMessage(nameof(IResourceEventListener.IResourceEventListener_Excute), "OnHit", SendMessageOptions.DontRequireReceiver);
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

        pStat?.DoInit(this);

        _pCollector.EventOnGetConsumable += _pCollector_EventOnGetConsumable;
    }

    private void _pCollector_EventOnGetConsumable(Consumable consumable)
    {
        CalculateDeltaStat(consumable.Effects.deltaStats);
    }


    // ====================================================================

    private void CalculateDeltaStat(List<DeltaStat> listDeltaStat)
    {
        foreach (var pDeltaStat in listDeltaStat)
        {
            switch (pDeltaStat.type)
            {
                case HomeKeeperBuffType.HP_Recovery:
                    pStat.DoRecovory(pDeltaStat.value);
                    break;
            }
        }
    }
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

        while (_pPhysicsTrigger.GetColliderList_3D_Enter().Count == 0)
        {
            yield return null;
        }

        Transform pTransformTarget = _pPhysicsTrigger.GetColliderList_3D_Enter()[0].transform;
        RaycastHit[] arrHit = Physics.RaycastAll(transform.position, (pTransformTarget.position - transform.position).normalized, pTargetLayer);
        for (int i = 0; i < arrHit.Length; i++)
        {
            CharacterModel pCharacterModel = arrHit[i].transform.GetComponent<CharacterModel>();
            if (pCharacterModel && pCharacterModel != this)
            {
                p_Event_OnSetTarget.DoNotify(pCharacterModel.gameObject);
                yield break;
            }
        }

        yield return null;
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

            List<Collider> listCollider = _pPhysicsTrigger.GetColliderList_3D_Enter();
            for (int i = 0; i < listCollider.Count; i++)
            {
                if (listCollider[i].GetComponent<CharacterModel>() != null)
                {
                    Transform pTransformTarget = listCollider[i].transform;
                    fDistance_ForDebug = Vector3.Distance(transform.position, pTransformTarget.position);
                    if (GetCurrentWeapon().DoCheck_IsReadyToFire(fDistance_ForDebug))
                    {
                        RaycastHit[] arrHit = Physics.RaycastAll(transform.position, (pTransformTarget.position - transform.position).normalized, pTargetLayer);
                        for (int j = 0; j < arrHit.Length; j++)
                            DoAttack_Melee(arrHit[j].transform.gameObject);

                        yield break;
                    }
                }
            }

            yield return null;
        }
    }

}
