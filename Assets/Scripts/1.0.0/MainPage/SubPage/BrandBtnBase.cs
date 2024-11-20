using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrandBtnBase : MonoBehaviour
{
    public virtual void Setting()
    {

    }
    public virtual void Active(bool state)
    {
        gameObject.SetActive(state);
    }
}
