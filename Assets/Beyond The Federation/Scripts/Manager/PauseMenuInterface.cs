using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuInterface : MonoBehaviour
{
    public PauseMenuSystem pause;

    // Start is called before the first frame update
    public void Idle()
    {
        pause.StartIdle();
    }

    public void Next()
    {
        pause.NextMenu_AnimationCallback();
    }
}
