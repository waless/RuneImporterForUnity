using UnityEngine;
using System.Collections.Generic;

public class TestScript : RuneImporter.RuneScriptableObject
{
    public List<Value> ValueList = new List<Value>();

    public class Value
    {
        public string name;
        public int number;
        public float position;
    }
}
