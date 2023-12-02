using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTriggerInterface : MonoBehaviour
{
    public int Identifier;

    public bool AmILab;

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
        if(other.tag == "Player")
        {
            TutorialInstructionsManager.instance.TriggerInstruction(Identifier);
            gameObject.SetActive(false);
        }
    }
}
