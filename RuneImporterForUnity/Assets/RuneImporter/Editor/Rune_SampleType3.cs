using System;
using System.Collections.Generic;
using UnityEngine;
using RuneImporter;

public class Rune_SampleType3 : RuneScriptableObject
{
    [SerializeField]
    public List<Value> ValueList = new List<Value>();

    [Serializable]
    public struct Value
    {
        public string name;
    }
}
