using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsPoints : MonoBehaviour
{
    public int pointsToAdd;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            ScoreManager.instance.AddPoints(pointsToAdd);
            //AudioManager.instance.PlaySFX("");
            Destroy(gameObject);
        }
    }
}
