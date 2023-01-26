using System;
using UnityEngine;
using RuneImporter;

public class Rune_SampleType4 : RuneScriptableObject
{
    [SerializeField]
    public Value[] ValueList = new Value[2];

    [Serializable]
    public struct Value
    {
        public string name;
    }
}
