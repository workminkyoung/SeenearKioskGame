using KioskApp.Cinema;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UC_Seat : MonoBehaviour
{
    public Action<string, UC_Seat> SelectSeat, DeselectSeat;

    [SerializeField]
    private bool _isOccupied = false;
    private TextMeshProUGUI _text;
    private Image _occupied;
    private Toggle _toggle;

    public Toggle Toggle => _toggle;
    public TextMeshProUGUI Text => _text;

    public void Init()
    {
        _toggle = GetComponent<Toggle>();
        _text = GetComponentInChildren<TextMeshProUGUI>();
        _occupied = UtilityExtensions.GetComponentOnlyInChildren_NonRecursive<Image>(transform);

        _toggle.onValueChanged.AddListener((state) =>
        {

            if (TextData._occupiedSeats.Contains(_text.text))
                return;
            
            if (state)
                SelectSeat(_text.text, this);
            else
                DeselectSeat(_text.text, this);

            SetOccupied(state);
        });
    }

    public void SetName(string name)
    {
        _text.text = name;
    }

    public void SetOccupied(bool occupied)
    {
        _isOccupied = occupied;
        _occupied.enabled = _isOccupied ? true : false;
    }
}
