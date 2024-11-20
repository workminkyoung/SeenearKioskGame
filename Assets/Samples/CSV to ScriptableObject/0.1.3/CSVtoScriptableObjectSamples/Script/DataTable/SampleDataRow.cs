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

[Serializable]
public class SampleDataRow : BaseDataTableRow
{
    public int IndexInt;
	public string NameString;
	public float LengthFloat;
	public Vector3 RandomVector3;
	public Vector2 RandomVector2;
	
    public override void SetData(Dictionary<string, object> data)
    {
        base.SetData(data);            

        IndexInt = ParseINT(data["IndexInt"].ToString());
		NameString = data["NameString"].ToString();
		LengthFloat = ParseFLOAT(data["LengthFloat"].ToString());
		RandomVector3 = ParseVector3(data["RandomVector3"].ToString());
		RandomVector2 = ParseVector2(data["RandomVector2"].ToString());
		

    }
}
