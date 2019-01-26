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
public class Spawner_Character : CObjectBase
{
    /* const & readonly declaration             */

    /* enum & struct declaration                */

    public enum ECharacterType
    {
        Player,
        Thief,
    }

    /* public - Field declaration            */

    public ECharacterType p_eCharacterType;

    /* protected & private - Field declaration         */


    // ========================================================================== //

    /* public - [Do] Function
     * 외부 객체가 호출(For External class call)*/

    [Button("DoSpawn_Character")]
    public void DoSpawn_Character(bool bUpdateForce)
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
            else if(pChildObject.name.Contains(p_eCharacterType.ToString_GarbageSafe()))
            {
                DestroyImmediate(pChildObject);
            }
        }

        if(transform.childCount == 0)
        {
            GameObject pObjectPrefab = GameObject.Instantiate(Resources.Load("Character/" + p_eCharacterType.ToString_GarbageSafe())) as GameObject;
            pObjectPrefab.transform.SetParent(transform);
            pObjectPrefab.transform.DoResetTransform();
        }
    }

    // ========================================================================== //

    /* protected - Override & Unity API         */

    protected override void OnAwake()
    {
        base.OnAwake();

        DoSpawn_Character(true);
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Application.isPlaying)
            return;

        DoSpawn_Character(false);
    }
#endif

    /* protected - [abstract & virtual]         */


    // ========================================================================== //

    #region Private

    #endregion Private
}