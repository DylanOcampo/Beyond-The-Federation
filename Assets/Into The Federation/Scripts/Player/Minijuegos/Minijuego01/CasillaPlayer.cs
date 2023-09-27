using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CasillaPlayer : MonoBehaviour
{
    public int puntaje = 0;
    public TextMeshProUGUI scoreText;

    void Start()
    {
        scoreText = GameObject.Find("ScoreUI").GetComponent<TextMeshProUGUI>();
    }
}
