using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloatedAnimations : MonoBehaviour
{
    [SerializeField] private Animator _anim;


    #region Encapsulamento
    public Animator Anim { get => _anim; set => _anim = value; }

    #endregion

    private void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
    }


    #region Move

    public void SetMovimentAnim(int moviment)      // Anima��o de movimenta��o
    {
        Anim.SetInteger("transition", moviment);
    }

    #endregion

    #region Attacks


    public void SetBoolIsAttacking(bool state) // Transition de Ataque
    {
        Anim.SetBool("isAttacking", state);
    }

    public void SetMeleeAttack()   // Anima��o de ataque corpo a corpo
    {
        Anim.Play("Bloated_Attack4", -1);
    }

    public void SetRangedAttack1() // Ataque a dist�ncia 1
    {
        Anim.Play("Bloated_Attack1", -1);
    }

    public void SetRangedAttack2()  // Ataque a dist�ncia 2
    {
        Anim.Play("Bloated_Attack3", -1);
    }

    #endregion

    #region Hit and Death

    public void SetHit()   // Anima��o de Dano
    {
        Anim.Play("Bloated_Hurt", -1);
    }


    public void SetDeath() // anima��o de Ataque 
    {
        Anim.Play("Bloated_Death", -1);
    }

    #endregion


}
