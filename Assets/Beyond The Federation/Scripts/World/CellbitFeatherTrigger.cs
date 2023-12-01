using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellbitFeatherTrigger : MonoBehaviour
{
    public OpenDoorMechanism mechanism;
    public int Identifier;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Feather")
        {
            if(Identifier == 1)
            {
                mechanism.Mechanism1 = true;
                mechanism.CheckForSuccess();
                
            }

            if (Identifier == 2)
            {
                mechanism.Mechanism2 = true;
                mechanism.CheckForSuccess();
            }

            if (Identifier == 3)
            {
                mechanism.Mechanism3 = true;
                mechanism.CheckForSuccess();
            }
            gameObject.transform.DORotate(new Vector3(0, 0, -70), 5);
            AudioManager.instance.PlayClip(28);
        }
    }


}
