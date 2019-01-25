using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ConsumableType {none = -1, jewel, aidKit }
public class Consumable : MonoBehaviour {
    public ConsumableType Type;
    public StatEffects Effects;

}
