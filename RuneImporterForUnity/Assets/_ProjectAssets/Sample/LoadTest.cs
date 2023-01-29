using System.Linq;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LoadTest : MonoBehaviour
{
    void Awake()
    {
        // テーブル型からロード
        //LoadFromType();

        // ロード関数から一括でロード
        LoadAll();
    }

    void LoadFromType()
    {
        var handle = Rune_SampleType.LoadInstanceAsync();
        handle.Completed += (handle) =>
        {
            var result = handle.Result as Rune_SampleType;
            foreach (var v in result.ValueList)
            {
                Debug.Log(v.name);
                Debug.Log(v.number);
                Debug.Log(v.position);
            }
        };
    }

    void LoadAll()
    {
        var loader_type = typeof(RuneImporter.RuneLoader);
        var methods = loader_type.GetMethods();
        var call_methods = methods.Where(m => m.IsPublic && m.IsStatic);
        foreach (var m in call_methods)
        {
            var result = m.Invoke(null, null);
            if (result != null && result is AsyncOperationHandle)
            {
                var handle = (AsyncOperationHandle)result;
                handle.Completed += (v) =>
                {
                    var s = v.Result as Rune_SampleType;
                    foreach (var value in s.ValueList)
                    {
                        Debug.Log(value.name);
                        Debug.Log(value.number);
                        Debug.Log(value.position);
                    }
                };
            }
            else
            {
                Debug.LogWarning($"RuneLoaderのメンバにAsyncOperationHandleを返さない関数が含まれています:{m.Name}");
            }
        }
    }
}
