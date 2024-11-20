using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVDataManager : SingletonBehaviour<CSVDataManager>
{
    public TutorialDataTable _TutorialData = new TutorialDataTable();
    protected override void Init()
    {
           
    }

    /*public T LoadCSVFile(string category, int index)
    {
        //_TutorialData.SetData(SimpleCSVReader.Read(path));
        string path = Application.streamingAssetsPath + "/1.1.0/" + category + "/TutoSo" + index + ".asset";
        
    }*/

    public void CreateScriptableCSV()
    {
        
    }
}
