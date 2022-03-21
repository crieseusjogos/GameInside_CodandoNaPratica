using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Animator))]
public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator anim;

    private void Awake()
    {
        //anim = GetComponentInChildren<Animator>();
        
    }

    public void SetMovementDirection(int direction)
    {
        anim.SetInteger("transition", direction);
    }

     
    public void SetAttack(int attackNumber)
    {
        if(attackNumber < 30)
        {
            anim.Play("Player_Attack1", -1);
        }
        else if(attackNumber > 29 && attackNumber < 70)
        {
            anim.Play("Player_Attack2", -1);
        }
        else if (attackNumber > 70)
        {
            anim.Play("Player_Attack3", -1);
        }
    }

}
