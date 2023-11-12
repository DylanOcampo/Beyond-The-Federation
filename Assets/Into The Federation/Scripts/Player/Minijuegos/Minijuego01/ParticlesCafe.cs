using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesCafe : MonoBehaviour
{
    public ParticleSystem particle;

    private void Start()
    {
        particle.Stop();
    }

}
