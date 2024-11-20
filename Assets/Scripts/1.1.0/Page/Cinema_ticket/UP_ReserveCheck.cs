using KioskApp.Cinema;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UP_ReserveCheck : UP_PageBase
{
    [SerializeField]
    private TextMeshProUGUI _textName, _textDetail, _textPrice;
    private RawImage _thumbnail;

    private const int _defaultPrice = 6000;

    public override void Init()
    {
        base.Init();
        _thumbnail = GetComponentInChildren<RawImage>();  
    }

    protected override void OnPageEnable()
    {
        base.OnPageEnable();
        Movie movie = Cinema_UserData.Instance._movie;
        string seatText = "";
        for (int i = 0; i < 2; i++)//max 2
        {
            if (movie._selecSeats.Count - 1 < i)
                continue;
            if (i == 0)
                seatText = movie._selecSeats[i];
            else
                seatText += "," + movie._selecSeats[i];
        }
        int price = 0;
        price = _defaultPrice * (movie._teenNum + movie._adultNum);

        _thumbnail.texture = movie._thumbnail;
        _textName.text = movie._name;
        _textDetail.text = string.Format(
            $"{System.DateTime.Now.ToString("yyyy")}년{System.DateTime.Now.ToString("MM")}월{System.DateTime.Now.ToString("dd")}일\n" +
            movie._time + "\n" +
            $"성인 {movie._adultNum}매, 청소년{movie._teenNum}매" + "\n" +
            $"{seatText}...({movie._selecSeats.Count}명)"
            );

        _textPrice.text = price.ToString() + "원";

        _navigation.ResetNextAction();
        _navigation.ActivateNextButton(true);
    }

    enum TEXT
    {
        DATE = 0,
        TIME,
        NUM,
        SEAT
    }
}
