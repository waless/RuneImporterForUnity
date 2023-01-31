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

    async void LoadAll()
    {
        await RuneImporter.RuneLoader.LoadAllAsync();

        var sample_data = Rune_SampleType.instance.ValueList;
        foreach (var v in sample_data)
        {
            Debug.Log(v.name);
            Debug.Log(v.number);
            Debug.Log(v.position);
        }
    }
}
