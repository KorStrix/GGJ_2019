using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterAttackButton : CObjectBase, IUIObject_HasButton<CharacterAttackButton.EUIButton>
{
    public enum EUIButton
    {
        Button,
    }

    CTweenPosition_Radial _pTweenRadial = null;

    protected override void OnAwake()
    {
        base.OnAwake();

        GetComponentInParent<Canvas>().worldCamera = HomeKeeperGameManager.instance.p_pInGameCamera;

    }


    protected override void OnEnableObject()
    {
        base.OnEnableObject();

        if (_pTweenRadial == null)
        {
            _pTweenRadial = GetComponentInParent<CTweenPosition_Radial>();
            if (_pTweenRadial != null)
                _pTweenRadial.p_Event_OnFinishTween_InCludeArg += _pTweenRadial_p_Event_OnFinishTween_InCludeArg;
        }
    }

    private void _pTweenRadial_p_Event_OnFinishTween_InCludeArg(CTweenBase.ETweenDirection eTweenDirection, CTweenBase pTweener)
    {
        if (eTweenDirection == CTweenBase.ETweenDirection.Reverse)
            _pTweenRadial.gameObject.SetActive(false);
    }

    public void IUIObject_HasButton_OnClickButton(EUIButton eButtonName)
    {
        if (HomeKeeperGameManager.instance.p_bIsWaitAction)
            return;

        switch (eButtonName)
        {
            case EUIButton.Button:
                GetComponentInParent<CTweenPosition_Radial>().DoPlayTween_Reverse();
                _pTweenRadial.DoPlayTween_Reverse();
                HomeKeeperGameManager.instance.Event_OnClickPlayerButton();
                break;
        }
    }
}
