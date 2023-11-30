using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : AppObject
{
    

    private static ScoreManager _instance;
    public static ScoreManager instance
    {
        get { 
            if( _instance == null )
            {
                _instance = GameObject.FindObjectOfType<ScoreManager>();
            }
            return _instance;
        }
    }

    public int score;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    public void AddPoints(int pointsToAdd)
    {
        score += pointsToAdd;
        InvokeEvent<ScoreChangeEvent>(new ScoreChangeEvent(score));
    }
}
