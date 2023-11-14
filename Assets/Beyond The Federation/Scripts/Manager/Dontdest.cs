using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Dontdest : MonoBehaviour
{
    public static Dontdest instance;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
}
