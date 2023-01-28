using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using RuneImporter;

public class Rune_SampleType3 : RuneScriptableObject
{
    public static Rune_SampleType3 instance { get; private set; }

    [SerializeField]
    public Value[] ValueList = new Value[2];

    [Serializable]
    public struct Value
    {
        public string name;
    }

    public static AsyncOperationHandle LoadInstanceAsync() {
        var path = Config.ScriptableObjectDirectory + "SampleType3.asset";
        var handle = Config.OnLoad(path);
        handle.Completed += (handle) => { instance = handle.Result as Rune_SampleType3; };

        return handle;
    }
}
