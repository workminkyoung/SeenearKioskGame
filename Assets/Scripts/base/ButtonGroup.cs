using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonGroup : Button
{
    [SerializeField]
    private List<Selectable> selectables = new List<Selectable>();

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        selectables.AddRange(UtilityExtensions.GetComponentsOnlyInChildren_NonRecursive<Selectable>(transform));
    }

    protected override void DoStateTransition(SelectionState state, bool instant)
    {
        base.DoStateTransition(state, instant);
        if(state == SelectionState.Selected)
        {

            for (int i = 0; i < selectables.Count; i++)
            {
                //selectables[i].dostate
            }
        }
        else if(state == SelectionState.Pressed)
        {

        }
    }

}

public class SelectableColor
{
    public Color colorPrimary;
    public Color colorPress;
}
