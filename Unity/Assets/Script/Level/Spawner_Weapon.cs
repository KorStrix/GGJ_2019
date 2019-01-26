#region Header
/*	============================================
 *	작성자 : Strix
 *	작성일 : 2019-01-26 오전 2:12:22
 *	개요 : 
   ============================================ */
#endregion Header

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;

/// <summary>
/// 
/// </summary>
[ExecuteInEditMode]
public class Spawner_Weapon : CObjectBase
{
    /* const & readonly declaration             */

    /* enum & struct declaration                */

    public enum EWeaponType
    {
        Bat,
        Sword,
        Colt_M1917,
        M36,
        AK47,
        Kar98K
    }

    /* public - Field declaration            */

    public EWeaponType p_eItemType;
    public bool p_randSpawn;

    /* protected & private - Field declaration         */


    // ========================================================================== //

    /* public - [Do] Function
     * 외부 객체가 호출(For External class call)*/

    [Button("DoSpawn_Item")]
    public void DoSpawn_Weapon(bool bUpdateForce)
    {
        while (transform.childCount > 1)
            DestroyImmediate(transform.GetChild(0).gameObject);

        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject pChildObject = transform.GetChild(0).gameObject;
            if (bUpdateForce)
            {
                DestroyImmediate(pChildObject);
            }
            else if(pChildObject.name.Contains(p_eItemType.ToString_GarbageSafe()) == false)
            {
                DestroyImmediate(pChildObject);
            }
        }

        if(transform.childCount == 0)
        {
            GameObject pObjectPrefab = GameObject.Instantiate(Resources.Load("Weapon/" + p_eItemType.ToString_GarbageSafe())) as GameObject;
            pObjectPrefab.transform.SetParent(transform);
            pObjectPrefab.transform.DoResetTransform();
        }
    }

    // ========================================================================== //

    /* protected - Override & Unity API         */

    protected override void OnAwake()
    {
        base.OnAwake();
        if(p_randSpawn)p_eItemType = (EWeaponType)Random.Range(0, 5.999f);
        DoSpawn_Weapon(false);
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Application.isPlaying)
            return;

        DoSpawn_Weapon(false);
    }
#endif

    /* protected - [abstract & virtual]         */


    // ========================================================================== //

    #region Private

    #endregion Private
}