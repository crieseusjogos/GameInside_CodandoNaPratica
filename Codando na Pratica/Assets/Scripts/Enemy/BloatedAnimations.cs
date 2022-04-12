using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloatedAnimations : MonoBehaviour
{
    [SerializeField] private Animator _anim;


    public Animator Anim { get => _anim; set => _anim = value; }

    private void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
    }


    public void SetMovimentAnim(int moviment)
    {
        Anim.SetInteger("transition", moviment);
    }



    public void SetMeleeAttack()
    {
        Anim.Play("Bloated_Attack4", -1);
    }

    public void  SetBoolIsAttacking( bool state)
    {
        Anim.SetBool("isAttacking", state);
    }




    public void SetRangedAttack1()
    {
        Anim.Play("Bloated_Attack1", -1);
    }

    public void SetRangedAttack2()
    {
        Anim.Play("Bloated_Attack3", -1);
    }


    public void SetHit()
    {
        Anim.Play("Bloated_Hurt", -1);
    }


    public void SetDeath()
    {
        Anim.Play("Bloated_Death", -1);
    }

}
