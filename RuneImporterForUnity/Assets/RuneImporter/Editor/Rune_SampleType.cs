using System;
using UnityEngine;
using RuneImporter;

public class Rune_SampleType : RuneScriptableObject
{
    [SerializeField]
    public Value[] ValueList = new Value[4];

    [Serializable]
    public struct Value
    {
        public string name;
        public int number;
        public float position;
    }
}
