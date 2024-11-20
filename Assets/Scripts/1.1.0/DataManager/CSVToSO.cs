using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;
using System.Text.RegularExpressions;


public class CSVToSO
{
    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    static char[] TRIM_CHARS = { '\"' };
    
    private static string CSVPath = "/1.1.0/MovieTicket.csv";
    //[MenuItem("Utilities/Generate SO")]
    //public static void GenerateEnemies()
    //{
    //    List<Dictionary<string, object>> tutoData = new List<Dictionary<string, object>>();
    //    tutoData = Read(Application.streamingAssetsPath + CSVPath);
    //    int index = 0;
    //    foreach(var dict in tutoData)
    //    {
    //        TutorialDataTable tuto = ScriptableObject.CreateInstance<TutorialDataTable>();
    //        tuto.SetData(dict);
    //        AssetDatabase.CreateAsset(tuto, $"Assets/CSV/TutoSO{index}.asset");
    //        index++;
    //    }
    //    AssetDatabase.SaveAssets();
    //}
    
    
    public static List<Dictionary<string, object>> Read(string filePath) 
    {
        var list = new List<Dictionary<string, object>>();

        string source;
        StreamReader sr = new StreamReader(Path.GetFullPath(filePath));
        source = sr.ReadToEnd();
        sr.Close();

        var lines = Regex.Split(source, @"###");

        if(lines.Length <= 1)
        {
            return null;
        }

        var header = Regex.Split(lines[0], SPLIT_RE);
        Debug.Log("head length " + header.Length);
        for(var i = 1; i < lines.Length; i++) {

            Debug.Log(lines[i]);
            var values = Regex.Split(lines[i], SPLIT_RE);
            if (values.Length == 0 || values[0] == "")
            {
                continue;
            }

            var entry = new Dictionary<string, object>();
            for(var j = 0; j < header.Length && j < values.Length; j++) {
                string value = values[j];
                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
                object finalvalue = value;
                int n;
                if(int.TryParse(value, out n)) {
                    finalvalue = n;
                }

                if (value.Length <= 0)
                    finalvalue = string.Empty;
                entry[header[j]] = finalvalue;
                Debug.Log(header[j]);
                Debug.Log(finalvalue);
            }
            list.Add(entry);
        }

        return list;
    }
}