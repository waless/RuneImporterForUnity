using UnityEngine;

public class LoadTest : MonoBehaviour
{
    void Awake()
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
}
