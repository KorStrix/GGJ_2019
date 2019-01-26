using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterAttackButton : CUIObjectBase, IUIObject_HasButton<CharacterAttackButton.EUIButton> {

    public enum EUIButton
    {
        Button,
    }

    protected override void OnAwake()
    {
        base.OnAwake();

        GetComponentInParent<Canvas>().worldCamera = HomeKeeperGameManager.instance.p_pInGameCamera;
    }
	
    public void IUIObject_HasButton_OnClickButton(EUIButton eButtonName)
    {
        switch (eButtonName)
        {
            case EUIButton.Button:
                GetComponentInParent<CTweenPosition_Radial>().DoPlayTween_Reverse();
                HomeKeeperGameManager.instance.Event_OnClickPlayerButton();
                break;
        }
    }
}
