using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable]
public class GameDataTable : BaseDataTableRow
{
    public int _level;
    public bool _random;
    public string _questRaw;
    public string _answerRaw;
    public List<string> _quest = new List<string>();
    public List<string> _answer = new List<string>();

    public override void SetData(Dictionary<string, object> data)
    {
        base.SetData(data);

        _level = ParseINT(data["level"].ToString());
        _random = (bool)data["random"];
        _questRaw = data["quest"].ToString();
        _answerRaw = data["answer"].ToString();

        _quest = new List<string>();
        foreach (string item in _questRaw.Split(','))
        {
            _quest.Add(item.Trim());
        }
        _answer = new List<string>();
        foreach(string item in _answerRaw.Split(","))
        {
            _answer.Add(item.Trim());
        }
    }
}
