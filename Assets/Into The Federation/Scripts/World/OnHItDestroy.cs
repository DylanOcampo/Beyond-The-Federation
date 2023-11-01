using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHItDestroy : MonoBehaviour
{
    public GameObject ShatterPrefab;
    public GameObject UnShatterPrefab;

    public float SecondsToDestroy = 3;
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
        if(other.gameObject.tag == "AttackCollider")
        {
            ShatterPrefab.SetActive(true);
            
            UnShatterPrefab.gameObject.SetActive(false);
            StartCoroutine(DestroySelf());
        }
    }

    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(SecondsToDestroy);
        Destroy(gameObject);
    }


}
