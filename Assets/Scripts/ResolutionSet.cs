using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionSet : MonoBehaviour
{
    //Default 해상도 비율
    float fixedAspectRatio = 9f / 16f;
    public CanvasScaler thisCanvas;
    private void Awake()
    {
        float currentAspectRatio = (float)Screen.width / (float)Screen.height;
        
        //현재 해상도 가로 비율이 더 길 경우
        if (currentAspectRatio > fixedAspectRatio) thisCanvas.matchWidthOrHeight = 1;       
        

        //현재 해상도의 세로 비율이 더 길 경우
        else if (currentAspectRatio < fixedAspectRatio) thisCanvas.matchWidthOrHeight = 0;
    }
}
