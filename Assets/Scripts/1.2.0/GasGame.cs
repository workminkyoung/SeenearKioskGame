using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GasGame : MonoBehaviour
{
    [SerializeField]
    private List<Image> _decors = new List<Image>();
    [SerializeField]
    private SwitchImage _gas;

    private void Awake()
    {
        
    }

    private void IntroAnimation()
    {

    }

    private void LoopingAnimation()
    {

    }

    private void StopAnimation()
    {

    }

    private enum Decor
    {
        None = -1,

        Stars,
        Moon,
        Cloud1,
        Cloud2,
        Window,
        Building,

        Length
    }
}
