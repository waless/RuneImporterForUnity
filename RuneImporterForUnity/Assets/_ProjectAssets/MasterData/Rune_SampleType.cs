using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using RuneImporter;

namespace RuneImporter
{
    public static partial class RuneLoader
    {
        public static AsyncOperationHandle Rune_SampleType_LoadInstanceAsync()
        {
            return Rune_SampleType.LoadInstanceAsync();
        }
    }
}

public class Rune_SampleType : RuneScriptableObject
{
    public static Rune_SampleType instance { get; private set; }

    [SerializeField]
    public Value[] ValueList = new Value[4];

    [Serializable]
    public struct Value
    {
        public string name;
        public int number;
        public float position;
    }

    public static AsyncOperationHandle<Rune_SampleType> LoadInstanceAsync() {
        var path = Config.ScriptableObjectDirectory + "SampleType.asset";
        var handle = Addressables.LoadAssetAsync<Rune_SampleType>(path);
        handle.Completed += (handle) => { instance = handle.Result; };

        return handle;
    }
}
