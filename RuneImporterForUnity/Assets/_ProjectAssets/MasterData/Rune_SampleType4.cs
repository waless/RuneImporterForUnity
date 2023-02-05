using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using RuneImporter;

namespace RuneImporter
{
    public static partial class RuneLoader
    {
        public static AsyncOperationHandle Rune_SampleType4_LoadInstanceAsync()
        {
            return Rune_SampleType4.LoadInstanceAsync();
        }
    }
}

public class Rune_SampleType4 : RuneScriptableObject
{
    public static Rune_SampleType4 instance { get; private set; }

    [SerializeField]
    public Value[] ValueList = new Value[2];

    [Serializable]
    public struct Value
    {
        public string name;
    }

    public static AsyncOperationHandle LoadInstanceAsync() {
        var path = Config.ScriptableObjectDirectory + "SampleType4.asset";
        var handle = Config.OnLoad(path);
        handle.Completed += (handle) => { instance = handle.Result as Rune_SampleType4; };

        return handle;
    }
}
