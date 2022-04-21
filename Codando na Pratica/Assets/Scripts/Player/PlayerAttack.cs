using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAttack : MonoBehaviour
{
    private PlayerAnimation playerAnimation;     // Armazena o Script que gerencia a Anima��o do Player
    private PlayerMoviment playerMoviment;       // Armazena o Script que gerencia o Movimento do Player
    private PlayerInput playerInput;             // Armazena o Script que gerencia os Inputs do Player

    private bool canAttack = true;               // Controla se o Player pode atacar

    private void Awake()
    {
        // Faz as referencias dos components

        playerAnimation = GetComponent<PlayerAnimation>();   
        playerMoviment = GetComponent<PlayerMoviment>();
        playerInput = GetComponent<PlayerInput>();

    }


    #region Attack

    public void SetAttack()  // Fun��o que faz a l�gica do ataque
    {
        if(canAttack)                                   // Verifica se pode atacar
        {
            canAttack = false;                          // Impede de executar a fun��o v�rias vezes   

            playerInput.isAttack = true;                // Sinaliza o ataque para o script de inputs
            playerMoviment.DisableControls();           // Chama a fun��o que desabilita temporariamente os controles

            int randomAttack = Random.Range(0, 100);    // Sorteoia um n�mero randomico para um ataque aleat�rio

            playerAnimation.SetAttack(randomAttack);    // Chama a fun��o que executa a nima��o de ataque
            Invoke("CanNewAttack", 0.7f)                // Chama a fun��a que finaliza o ataque, esperando em m�dia o tempo das anima��es
;        }

    }


    void CanNewAttack()  // Fun��o que finaliza o ataque
    {
        canAttack = true;                             // Habilita para um novo ataque

        playerInput.isAttack = false;                 // Sinaliza o fim do ataque para o script de inputs
        playerMoviment.EnabledControls();             // habilita os controles do player
    }

    #endregion


}
