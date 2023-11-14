using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMiniGame : MonoBehaviour
{
    public bool BadAnswer1, BadAnswer2;
    public bool GoodAnswer1, GoodAnswer2;

    public GameObject GoodFence, BadFence;
    public GameObject GoodFenceFinalPosition, BadFenceFinalPosition;

    public void Register()
    {
        if(BadAnswer1 &&  BadAnswer2)
        {
            BadFence.transform.DOMove(BadFenceFinalPosition.transform.position, 2);

        }

        if(GoodAnswer1 && GoodAnswer2)
        {
            GoodFence.transform.DOMove(GoodFenceFinalPosition.transform.position, 2);
        }
    }


}
