using System;
using System.IO;
using UnityEngine;
using UnityEditor.AssetImporters;

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

    public override void OnImportAsset(AssetImportContext ctx)
    {
        using (var stream = new StreamReader(ctx.assetPath))
        {
            var json = stream.ReadToEnd();
            var book = JsonUtility.FromJson<RuneBook>(json);
            Debug.Log(json);
        }
    }
}
