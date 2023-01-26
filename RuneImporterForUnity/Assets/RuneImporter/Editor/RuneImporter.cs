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

        const string ClassPrefix = "Rune_";
        const string ValueListName = "ValueList";

        public override void OnImportAsset(AssetImportContext ctx)
        {
            using (var stream = new StreamReader(ctx.assetPath))
            {
                var json = stream.ReadToEnd();
                var book = JsonUtility.FromJson<RuneBook>(json);

                createInstanceAndSetting(book);
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
                var instance_type_name = instance.GetType().FullName;

                var value_type_name = instance_type_name + "+" + "Value";
                var value_type = Type.GetType(value_type_name);

                var value_list_type_name = value_type_name + "[]";
                var value_list_type = Type.GetType(value_list_type_name);
                var value_list_info = instance.GetType().GetField(ValueListName);
                var value_list_instance = (Array)value_list_info.GetValue(instance);

                for (int i = 0; i < table.Values.Length; ++i)
                {
                    var col_value_array = table.Values[i];
                    var value_instance = Activator.CreateInstance(value_type);
                    for (int j = 0; j < col_value_array.Values.Length; ++j)
                    {
                        var type = table.Types[j];
                        if (type.TypeName.Kind == "enum")
                        {
                            continue;
                        }

                        var value_string = col_value_array.Values[j];
                        var value_object = nameToObjectValue(type, value_string);
                        var value_name = type.TypeName.Value;
                        var value_field = value_type.GetField(value_name);

                        value_field.SetValue(value_instance, value_object);
                    }
                    value_list_instance.SetValue(value_instance, i);
                }
                Directory.CreateDirectory(Config.ScriptableObjectDirectory);
                AssetDatabase.CreateAsset(instance, Config.ScriptableObjectDirectory + table.Name + ".asset");
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
            return ClassPrefix + table.Name;
        }

        object nameToObjectValue(RuneType type, string value_name)
        {
            switch (type.TypeName.Kind)
            {
                case "int":
                    return parseIntValue(value_name);
                case "float":
                    return parseFloatValue(value_name);
                case "string":
                    return value_name;
            }

            return null;
        }

        static int parseIntValue(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return 0;
            }

            int result = 0;
            if (int.TryParse(str, out result))
            {
                return result;
            }
            else
            {
                Debug.LogError($"値が整数ではありません:{str}");
                return 0;
            }
        }

        static float parseFloatValue(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return 0f;
            }

            float result = 0f;
            if (float.TryParse(str, out result))
            {
                return result;
            }
            else
            {
                Debug.LogError($"値が浮動小数ではありません:{str}");
                return 0f;
            }
        }
    }
}
