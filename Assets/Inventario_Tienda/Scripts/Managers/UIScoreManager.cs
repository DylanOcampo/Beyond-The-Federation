using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIScoreManager : AppObject
{
    public TextMeshProUGUI PointsText;

    private void Start()
    {
        PointsText.text = "Coins: 0";
    }

    private void OnEnable()
    {
        AddEventListener<ScoreChangeEvent>(PointsChangedEventListener);
    }

    private void OnDisable()
    {
        RemoveEventListener<ScoreChangeEvent>(PointsChangedEventListener);
    }

    private void PointsChangedEventListener(ScoreChangeEvent _event)
    {
        PointsText.text = "Coins: " + _event.newPoints.ToString();
    }

}
