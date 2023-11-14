using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailFollow : MonoBehaviour
{
    public Transform PositionTogo;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.DOMove(PositionTogo.position, 5).SetLoops(-1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
