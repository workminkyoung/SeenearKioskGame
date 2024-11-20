using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KioskApp.Tutorial;

//TODO : namespace로 묶고 클래스명 바꿔서 카테고리별 데이터셋으로 사용하기
//[CreateAssetMenu(fileName = "tuto_", menuName = "Assets/CSV")]
[Serializable]
public class TutorialDataTable : BaseDataTableRow
{
    public int _index;
    public string _textNarration;
    public RectTransform _highlighting;
    public int _pointIndex;
    public POINT_SIZE _pointSize;
    public string _textAlert;

    public override void SetData(Dictionary<string, object> data)
    {
        base.SetData(data);

        _index = ParseINT(data["index"].ToString());
        _textNarration = data["textNarration"].ToString();
        _highlighting = null;
        _pointIndex = ParseINT(data["pointIndex"].ToString());
        _pointSize = (POINT_SIZE)ParseINT(data["pointSize"].ToString());
        if(data.ContainsKey("textAlert") && data["textAlert"] != null)
            _textAlert = data["textAlert"].ToString();
    }
}


//[Serializable]
//public class RealQuestDataTable
//{
//    public string _name;
//    public string _time;
//    public int _au
//}

