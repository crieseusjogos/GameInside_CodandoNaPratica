using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator anim;                // Armazena o animator do Player

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();        // Busca o component Animator no objeto filho
        
    }


    #region Moviment


    public void SetMovementDirection(int direction)      // Fun��o que recebe um int para alternar entre as anima��es idle e walk
    {
        anim.SetInteger("transition", direction);       // Se for 1 executa walk, se dor 0 executa idle
    }

    public void SetOnGround(bool onGround)              // Fun��o que controla se est� no ch�o ou n�o
    {
        anim.SetBool("onGround", onGround);      
    }


    #endregion

    #region Attack

    public void SetAttack(int attackNumber)              //Fun��o que recebe um valor randomico de 0 a 100, para escolher um ataque dentre os 3
    {
        if(attackNumber < 30)                            // Se o valor for menor que 30, executa o ataque 1
        {
            anim.Play("Player_Attack1", -1);
        }
        else if(attackNumber > 29 && attackNumber < 70)  // Se o valor for entre 30 e 69, executa o ataque 2
        {
            anim.Play("Player_Attack2", -1);
        }
        else if (attackNumber >= 70)                     // Se o valor for maior que 70, executa o ataque 3
        {
            anim.Play("Player_Attack3", -1);
        }
    }

    #endregion

    #region Damage


    public void SetHit()  // Executa a anima��o de Dano
    {
        anim.Play("Player_Hit", -1);
    }

    public void SetDeath()  // Executa a anima��o de morte
    {
        anim.Play("Player_Death", -1);
    }

    #endregion


}
