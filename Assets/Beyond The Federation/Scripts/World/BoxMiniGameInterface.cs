using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMiniGameInterface : MonoBehaviour
{
    public BoxMiniGame minigame;

    public bool AmICorrectAnswer;

    public bool AmINumber1;

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
        if(other.tag == "Box")
        {
            
            if (AmICorrectAnswer)
            {
                if (AmINumber1)
                {
                    minigame.GoodAnswer1 = true;

                }
                else
                {
                    minigame.GoodAnswer2 = true;
                }
            }
            else
            {
                if (AmINumber1)
                {
                    minigame.BadAnswer1 = true;
                }
                else
                {
                    minigame.BadAnswer2 = true;
                }
            }
            minigame.Register();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Box")
        {
            
            if (AmICorrectAnswer)
            {
                if (AmINumber1)
                {
                    minigame.GoodAnswer1 = false;

                }
                else
                {
                    minigame.GoodAnswer2 = false;
                }
            }
            else
            {
                if (AmINumber1)
                {
                    minigame.BadAnswer1 = false;
                }
                else
                {
                    minigame.BadAnswer2 = false;
                }
            }
            
        }
    }

}
