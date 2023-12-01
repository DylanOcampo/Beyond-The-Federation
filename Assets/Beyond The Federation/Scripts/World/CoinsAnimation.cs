using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CoinsAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.DORotate(new Vector3(0,180,0), 10).SetLoops(-1).SetEase(Ease.Linear);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
