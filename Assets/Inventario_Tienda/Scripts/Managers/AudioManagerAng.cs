using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class AudioRef
{
    public string name;
    public AudioClip audioClip;
}

public class AudioManagerAng : MonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<AudioManager>();
            }
            return _instance;
        }
    }

    private static AudioManagerAng dontDestroyAudioManager;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if(dontDestroyAudioManager == null)
        {
            dontDestroyAudioManager = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public AudioMixer audioMixer;

    public AudioSource byAudioSource;
    public AudioSource sfxAudioSource;

    public List<AudioRef> byMusicList = new List<AudioRef>();
    public List<AudioRef> sfxMusicList = new List<AudioRef>();

    private bool isPause = false;

    public void Start()
    {
        PlayBGMusic("Main");
    }

    public void PlayBGMusic(string name)
    {
        foreach (AudioRef reference in byMusicList)
        {
            if (reference.name == name)
            {
                byAudioSource.clip = reference.audioClip;
                byAudioSource.Play();
                break;
            }
        }
    }

    public void PlaySFX(string name)
    {
        foreach (AudioRef reference in sfxMusicList)
        {
            if (reference.name == name)
            {
                sfxAudioSource.clip = reference.audioClip;
                sfxAudioSource.Play();
                break;
            }
        }
    }

    public void PauseUnPauseBGMusic()
    {
        if (isPause)
        {
            byAudioSource.UnPause();
        }
        else
        {
            byAudioSource.Pause();
        }

        isPause = !isPause;
    }

}
