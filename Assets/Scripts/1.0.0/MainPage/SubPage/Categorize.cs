using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Categorize : MonoBehaviour
{
    public List<GameObject> contentList = new List<GameObject>();
    public GameObject emptyPopup;
    private void OnEnable()
    {
        if(contentList.Count == 0)
        {
            emptyPopup.SetActive(true);
        }
        else
        {
            emptyPopup.SetActive(false);
            for(int i = 0; i < contentList.Count; i++)
            {
                contentList[i].SetActive(true);
            };
        }
    }

    private void OnDisable()
    {
        for(int i = 0; i < contentList.Count; i++)
        {
            contentList[i].SetActive(false);
        };    
    }
}
