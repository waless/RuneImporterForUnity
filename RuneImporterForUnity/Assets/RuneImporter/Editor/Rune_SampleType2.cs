using System;
using UnityEngine;
using RuneImporter;

public class Rune_SampleType2 : RuneScriptableObject
{
    [SerializeField]
    public Value[] ValueList = new Value[3];

    [Serializable]
    public struct Value
    {
        public string name;
    }
}
