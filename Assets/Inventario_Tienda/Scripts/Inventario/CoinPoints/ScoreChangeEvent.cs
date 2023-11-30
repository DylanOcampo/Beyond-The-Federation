using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreChangeEvent : AppEvent
{
    public int newPoints;

    public ScoreChangeEvent(int _newPoints) : base(_newPoints) //Construcctor
    {
        newPoints = _newPoints;
    }
}
