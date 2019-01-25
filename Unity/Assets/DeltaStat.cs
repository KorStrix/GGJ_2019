using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum Stats { }

public class DeltaStat : ScriptableObject {
    public List<KeyValuePair<Stats, float>> Stats;
	
}
