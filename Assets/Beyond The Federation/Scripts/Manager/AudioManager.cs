using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;

    public static AudioManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<AudioManager>();
            }
            return _instance;
        }
    }

    public AudioSource Music, Sounds, Music1, Music2, Music3;
    public AudioClip[] sounds = new AudioClip[20];
    public AudioClip[] music = new AudioClip[20];
    public GameObject FootSteps;


    List<string> subs;

    private GameObject SubsFinalPosition, SubsText, timeobject;
    Tweener SubsAni;
    private int SubsPosition;
    private AudioClip audioEcplise;
    Vector3 Pos;



    void Update()
    {

    }

    public void setObjects(GameObject _SubsFinalPosition, GameObject _SubsText, GameObject _timeobject)
    {

        SubsFinalPosition = _SubsFinalPosition;
        SubsText = _SubsText;
        timeobject = _timeobject;

    }
    public void PlayClipMusic(int i)
    {
        Music1.gameObject.SetActive(false);
        Music2.gameObject.SetActive(false);
        Music3.gameObject.SetActive(false);

        if ( i == 0)
        {
            Music1.gameObject.SetActive(true);
        }
       if(  i == 1)
        {
            Music2.gameObject.SetActive(true);
        }
       if( i == 2)
        {
            Music3.gameObject.SetActive(true);
        }
    }



    public void PlayFootSteps()
    {
        FootSteps.SetActive(true);
    }

    public void StopFootSteps()
    {
        FootSteps.SetActive(false);
    }

    public void PlayClip(int i)
    {
        Sounds.PlayOneShot(sounds[i]);
        
    }

    public void Subs(List<string> _subs, AudioClip _audio)
    {
        audioEcplise = _audio;
        Sounds.PlayOneShot(audioEcplise);
        Pos = timeobject.transform.position;
        SubsAni = SubsText.transform.DOMove(SubsFinalPosition.transform.position, .5f).Pause().SetAutoKill(false);
        subs = _subs;

        SubsPosition = 0;
        SubsLogic();

    }



    private void SubsLogic()
    {
        if (subs[SubsPosition] == "")
        {
            SubsPosition++;
            SubsLogic();
        }
        else
        {
            timeobject.transform.position = Pos;
            SubsText.GetComponentInChildren<TextMeshProUGUI>().text = subs[SubsPosition];
            SubsAni.Play();
            SubsAni.OnComplete(() => {
                timeobject.transform.DOMove(SubsFinalPosition.transform.position, audioEcplise.length / subs.Count).OnComplete(() => {
                    SubsAni.Rewind();
                    SubsPosition++;
                    SubsLogic();

                }); ;


            });
        }

    }





    public void OnvalChangeMusic(float val)
    {
        Music.volume = val;
        Music1.volume = val;
        Music2.volume = val;
        Music3.volume = val;

    }
    public void OnvalChangeSFX(float val)
    {
        Sounds.volume = val;
    }

}