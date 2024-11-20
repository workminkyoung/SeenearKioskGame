using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class McTutoManager : MonoBehaviour
{
    public McdonaldManager manager;
    private TextBox _textBox;
    private LightPoint _lightPoint;
    private SideBar _sideBar;
    private int _curNarration = 0;

    private void Awake()
    {
        _textBox = GetComponentInChildren<TextBox>();
        _textBox.Setting(NextExplain);

        _lightPoint = GetComponentInChildren<LightPoint>();
        _lightPoint.Setting();

        _sideBar = GetComponentInChildren<SideBar>();
    }

    // Start is called before the first frame update
    void Start()
    {
        NextExplain();
    }

    void NextExplain()
    {
        McTuto tutoData = McdonaldProperties._McTuto.items[_curNarration];


        if (tutoData.next)
        {
           return; 
        }

        _textBox.SetTextInfo(tutoData.sizeVec, tutoData.posVec, tutoData.text);
        _lightPoint.SetState(tutoData.light);
        
        _curNarration++;
    }
    
    void NextExplain(McTuto tutoData)
    {
        _textBox.SetTextInfo(tutoData.sizeVec, tutoData.posVec, tutoData.text);
        _lightPoint.SetState(tutoData.light);
    }

    public void SetTutoSidebar(bool state)
    {
        _sideBar.gameObject.SetActive(state);
    }

    enum eTutoStep
    {
        Start = 0
    }
}
