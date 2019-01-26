using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextController : MonoBehaviour {

    private static FloatingText popupText;
    private static GameObject canvas;

    public static void Initialize()
    {
        canvas = GameObject.Find("ManagerUI");
        if (popupText == null)
        {
            popupText = Resources.Load<FloatingText>("PopupTextParent");
        }
    }
    public static void CreateFloatingText(string text, Vector3 v)
    {
        Initialize();
        FloatingText instance = Instantiate(popupText);

        instance.transform.SetParent(canvas.transform, true);
        instance.transform.position = new Vector3(v.x+5, v.y, v.z+5);
        
        instance.SetText(text);
        instance.indicator.color = Color.red;
    }
}