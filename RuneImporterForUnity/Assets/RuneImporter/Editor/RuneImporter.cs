using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.AssetImporters;

namespace RuneImporter
{
    public class RuneScriptableObject : ScriptableObject
    {
    }

    [ScriptedImporter(1, "rune")]
    public class RuneImporter : ScriptedImporter
    {
        [Serializable]
        struct RuneBook
        {
            public string Name;
            public RuneSheet[] Sheets;
        }

        [Serializable]
        struct RuneSheet
        {
            public string Name;
            public RuneTable[] Tables;
        }

        [Serializable]
        struct RuneTable
        {
            public string Name;
            public RuneType[] Types;
            public RuneValue[] Values;
        }

        [Serializable]
        struct RuneValue
        {
            public string[] Values;
        }

        [Serializable]
        struct RuneType
        {
            public RuneName TypeName;
        }

        [Serializable]
        struct RuneName
        {
            public string Kind;
            public string Value;
        }

        const string ValueListName = "ValueList";

        public override void OnImportAsset(AssetImportContext ctx)
        {
            using (var stream = new StreamReader(ctx.assetPath))
            {
                var json = stream.ReadToEnd();
                var book = JsonUtility.FromJson<RuneBook>(json);

                //createInstanceAndSetting(book);

                dynamic obj = ScriptableObject.CreateInstance("TestScript");

                var value_type = Type.GetType("TestScript.Value");

                AssetDatabase.CreateAsset(obj, "Assets/TestScriptObj.asset");
                AssetDatabase.Refresh();
            }
        }

        void createInstanceAndSetting(RuneBook book)
        {
            Array.ForEach(book.Sheets, (s) => createInstanceAndSetting(s));
        }

        void createInstanceAndSetting(RuneSheet sheet)
        {
            Array.ForEach(sheet.Tables, (t) => createInstanceAndSetting(t));
        }

        void createInstanceAndSetting(RuneTable table)
        {
            var instance = createInstance(table);
            if (instance != null)
            {
                var value_list_info = instance.GetType().GetField(ValueListName);
                dynamic value_list = value_list_info.GetValue(instance);

                for (int i = 0; i < table.Values.Length; ++i)
                {
                    var col_value_array = table.Values[i];
                    for (int j = 0; j < col_value_array.Values.Length; ++j)
                    {
                        var type = table.Types[j];
                        var value_string = col_value_array.Values[j];
                        var value_object = nameToObjectValue(type, value_string);
                        var value_name = type.TypeName.Value;

                        var value_type_name = "MasterData." + type.TypeName.Value + "." + "Value";

                        Type.GetType(value_type_name);
                    }
                }

                var mem = instance.GetType().GetField("");
                mem.SetValue(instance, "");
            }
        }

        RuneScriptableObject createInstance(RuneTable table)
        {
            var class_name = makeClassName(table);
            var instance = ScriptableObject.CreateInstance(class_name) as RuneScriptableObject;

            return instance;
        }

        string makeClassName(RuneTable table)
        {
            var namespace_name = "MasterData";

            return namespace_name + "." + table.Name;
        }

        object nameToObjectValue(RuneType type, string value_name)
        {
            switch (type.TypeName.Kind)
            {
                case "int":
                    return Convert.ToInt32(value_name);
                case "float":
                    return (float)Convert.ToDouble(value_name);
                case "string":
                    return value_name;
            }

            return null;
        }
    }
}
