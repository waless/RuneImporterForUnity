using System;
using System.Collections.Generic;
using UnityEngine;
using RuneImporter;

public class Rune_SampleType : RuneScriptableObject
{
    [SerializeField]
    public List<Value> ValueList = new List<Value>();

    [Serializable]
    public class Value
    {
        public string name;
        public int number;
        public float position;
    }
}
