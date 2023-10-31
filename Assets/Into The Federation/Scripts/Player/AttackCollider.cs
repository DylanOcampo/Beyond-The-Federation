using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{

    public GameObject attackCollider;
    public RoierPlayer Player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Animation_AttackColliderOn()
    {
        attackCollider.SetActive(true);
    }

    public void Animation_AttackColliderOff()
    {
        attackCollider.SetActive(false);
        Player.CanAttack = true;
    }
}
