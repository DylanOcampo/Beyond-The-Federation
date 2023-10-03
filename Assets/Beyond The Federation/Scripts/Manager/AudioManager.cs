using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource source;
    public Animator _animator;
    public AudioClip[] clip = new AudioClip[20];

    void Update()
    {
        
    }
    public void PlayClip(int i)
    {
        source.PlayOneShot(clip[i]);
    }
     

    public void Shooting()
    {
        PlayClip(1);
    }
    public void Walking()
    {
        PlayClip(2);
    }
    public void Atacking()
    {
        PlayClip(Random.Range(6, 8));
    }

    public void Jump()
    {
        PlayClip(3);
    }

    public void Roll()
    {
        Debug.Log("Roll");
        PlayClip(4);
    }
    public void Hit()
    {
        PlayClip(0);
    }
    public void Heal()
    {
        PlayClip(5);
    }
    public void HitSpider()
    {
        PlayClip(9);

    }
    public void Spidersound()
    {
        PlayClip(10);
    }
    public void StepsSpider()
    {
        PlayClip(11);
    }

}
