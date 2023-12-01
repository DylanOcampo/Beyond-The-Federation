using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinMapInterface : MonoBehaviour
{
    public int LocationIdentifier;
    public MiniMapManager _MiniMapManager;

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
        _MiniMapManager.ChangeTo(LocationIdentifier);
        if(LocationIdentifier == 2)
        {

        }
    }
}
