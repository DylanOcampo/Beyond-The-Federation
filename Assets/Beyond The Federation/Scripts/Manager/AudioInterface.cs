using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioInterface : MonoBehaviour
{
    public void OnvalChangeMusic(float val)
    {
        AudioManager.instance.OnvalChangeMusic(val);
    }
    public void OnvalChangeSFX(float val)
    {
        AudioManager.instance.OnvalChangeSFX(val);
    }
}
