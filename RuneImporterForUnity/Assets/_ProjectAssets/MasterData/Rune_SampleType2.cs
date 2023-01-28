using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using RuneImporter;

public class Rune_SampleType2 : RuneScriptableObject
{
    public static Rune_SampleType2 instance { get; private set; }

    [SerializeField]
    public Value[] ValueList = new Value[3];

    [Serializable]
    public struct Value
    {
        public string name;
    }

    public static AsyncOperationHandle LoadInstanceAsync() {
        var path = Config.ScriptableObjectDirectory + "SampleType2.asset";
        var handle = Config.OnLoad(path);
        handle.Completed += (handle) => { instance = handle.Result as Rune_SampleType2; };

        return handle;
    }
}
