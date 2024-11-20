using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class McdonaldProperties : MonoBehaviour
{
    public static McTutoList _McTuto;
    public const string ltype_normal = "";
    public const string ltype_scroll = "scroll";
}

[Serializable]
public class McTuto
{
    public int step;
    public string text;
    public string pos;
    public Vector2 posVec;
    public string size;
    public Vector2 sizeVec;
    public bool wait;
    public bool light;
    public string lpos = string.Empty;
    public Vector2 lposVec;
    public string ltype = string.Empty;
    public int mask;
    public bool next;
    public bool directaction;
    public bool btnaction;
}
    
[Serializable]
public class McTutoList
{
    public McTuto[] items;
}
