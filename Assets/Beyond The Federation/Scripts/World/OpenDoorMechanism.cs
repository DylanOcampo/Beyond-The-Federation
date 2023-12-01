using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorMechanism : MonoBehaviour
{
    // Start is called before the first frame update
   public bool Mechanism1, Mechanism2, Mechanism3;

    public GameObject DoorLeft, DoorRight;

    public void CheckForSuccess()
    {
        if(Mechanism1 && Mechanism2 && Mechanism3)
        {
            DoorLeft.transform.DORotate(new Vector3(0, -90, 0), 5);
            DoorRight.transform.DORotate(new Vector3(0, 90, 0), 5);
        }
    }


}
