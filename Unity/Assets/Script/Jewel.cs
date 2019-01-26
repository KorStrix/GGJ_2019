using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jewel : MonoBehaviour {

    public CObserverSubject p_Event_OnStolen { get; private set; } = new CObserverSubject();

    public string Name;
    public float Value;

    public bool p_bIsStolen { get; private set; }

    public void DoSet_Stolen(bool bStolen)
    {
        if (bStolen)
        {
            p_Event_OnStolen.DoNotify();
            p_Event_OnStolen.DoClear_Listener();
        }

        p_bIsStolen = bStolen;
    }
}
