using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{

    public GameObject attackColliderLeft, attackColliderRight;
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
        if (!Player.PlayerSpriteRenderer.flipX)
        {
            attackColliderRight.SetActive(true);
        }
        else
        {
            attackColliderLeft.SetActive(true);
        }
        
    }

    public void Animation_AttackColliderOff()
    {
        Player.RecoilAttack();
        attackColliderRight.SetActive(false);
        attackColliderLeft.SetActive(false);
        Player.CanAttack = true;
    }
}
