using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KioskApp.Cinema;
using UnityEngine.UI;
using TMPro;

//TODO : make selectable base
public class UC_MovieSelectable : MonoBehaviour
{
    private Movie _movie = new Movie();
    private RawImage _thumbnail;
    private TextMeshProUGUI _title;

    public Movie Movie => _movie;

    public void Init()
    {
        _thumbnail = GetComponentInChildren<RawImage>();
        _title = GetComponentInChildren<TextMeshProUGUI>();
        SetMovieData();
    }

    private void SetMovieData()
    {
        _movie._thumbnail = _thumbnail.texture;
        _movie._name = _title.text;
    }
}
