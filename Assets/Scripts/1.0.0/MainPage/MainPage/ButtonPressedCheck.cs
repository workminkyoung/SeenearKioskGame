using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonPressedCheck : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    bool isPressed;

    public GameObject pressedImg;

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
    }

    private void Update()
    {
        if(isPressed)
        {
            pressedImg.SetActive(true);
        }
        else
        {
            pressedImg.SetActive(false);
        }   
    }

    private void OnDisable()
    {
        pressedImg.SetActive(false);        
    }
}
