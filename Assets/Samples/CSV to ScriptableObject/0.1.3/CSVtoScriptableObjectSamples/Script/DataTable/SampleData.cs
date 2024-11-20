using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

///
/// !!! Machine generated code !!!
///
/// A class which deriveds ScritableObject class so all its data 
/// can be serialized onto an asset data file.
/// 

[CreateAssetMenu(menuName = "DataTable/SampleData")]
public class SampleData : BaseDataTable
{
#if UNITY_EDITOR
    [CustomEditor(typeof(SampleData))]
    public class SampleDataEditor : BaseDataTableEditor
    { 
        protected override string GetPath() 
        {
            return "D:/repos/4by4TempProj/CSV/Sample.csv";
        }
    }
#endif

}
