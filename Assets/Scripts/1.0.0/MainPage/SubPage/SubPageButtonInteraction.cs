using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SubPageButtonInteraction : MonoBehaviour
{
    public GameObject[] pageArray;
    public List<GameObject> categoryOnTxtList = new List<GameObject>();

    List<GaugeCTR> listCTR = new List<GaugeCTR>();

    void Awake()
    {
        //setting contetn prefabs
        listCTR.AddRange(GetComponentsInChildren<GaugeCTR>());
        for (int i = 0; i < listCTR.Count; i++)
        {
            listCTR[i].Setting();
        }
    }

    public void StartScene(string scenename)
    {
        print(scenename + "씬이동");
        SceneManager.LoadScene(scenename);
    }

    public void ToggleOn(int id)
    {
        for (int i = 0; i < categoryOnTxtList.Count; i++)
        {
            categoryOnTxtList[i].SetActive(false);
        };

        categoryOnTxtList[id].SetActive(true);
    }

    public void Back()
    {
        PageManager.instance.PageMove(1);
    }

    private void OnEnable()
    {
        for (int i = 0; i < categoryOnTxtList.Count; i++)
        {
            categoryOnTxtList[i].SetActive(false);
        };

        categoryOnTxtList[0].SetActive(true);

        // set content prefab state
        pageArray[(int)PageManager.instance.CurTrainngType].SetActive(true);
        for (int i = 0; i < listCTR.Count; i++)
        {
            listCTR[i].SetContentState(PageManager.instance.CurTrainngType);
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < pageArray.Length; i++)
        {
            pageArray[i].SetActive(false);
        };
    }
}
