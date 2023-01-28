using System;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace RuneImporter
{
    public static class Config
    {
        // ScriptableObjectアセットの出力先ディレクトリ
        // 環境により書き換えてください
        public const string ScriptableObjectDirectory = "Assets/_ProjectAssets/MasterData/";

        // ScriptableObjectのクラスが属するアセンブリ名
        public const string AssemblyName = "Assembly-CSharp";

        // ScriptableObjectアセットのロード方法
        // デフォルトではAddressableを使用します。プロジェクト方針により書き換えてください
        public static Func<string, AsyncOperationHandle> OnLoad = (path) =>
        {
            return Addressables.LoadAssetAsync<Rune_SampleType>(path);
        };
    }
}
