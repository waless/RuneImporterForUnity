using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace RuneImporter
{
    public static partial class RuneLoader
    {
        public static async Task LoadAllAsync()
        {
            var loader_type = typeof(RuneImporter.RuneLoader);
            var methods = loader_type.GetMethods();
            var call_methods = methods.Where(m => m.IsPublic && m.IsStatic && m.Name != "LoadAllAsync");

            var task_list = new List<Task>(call_methods.Count());
            foreach (var m in call_methods)
            {
                var handle = (AsyncOperationHandle)m.Invoke(null, null);
                task_list.Add(handle.Task);
            }

            await Task.WhenAll(task_list);
        }
    }
}
