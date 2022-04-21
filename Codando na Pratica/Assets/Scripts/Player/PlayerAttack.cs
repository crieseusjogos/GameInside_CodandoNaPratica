using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAttack : MonoBehaviour
{
    private PlayerAnimation playerAnimation;     // Armazena o Script que gerencia a Animação do Player
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

    public void SetAttack()  // Função que faz a lógica do ataque
    {
        if(canAttack)                                   // Verifica se pode atacar
        {
            canAttack = false;                          // Impede de executar a função várias vezes   

            playerInput.isAttack = true;                // Sinaliza o ataque para o script de inputs
            playerMoviment.DisableControls();           // Chama a função que desabilita temporariamente os controles

            int randomAttack = Random.Range(0, 100);    // Sorteoia um número randomico para um ataque aleatório

            playerAnimation.SetAttack(randomAttack);    // Chama a função que executa a nimação de ataque
            Invoke("CanNewAttack", 0.7f)                // Chama a funçõa que finaliza o ataque, esperando em média o tempo das animações
;        }

    }


    void CanNewAttack()  // Função que finaliza o ataque
    {
        canAttack = true;                             // Habilita para um novo ataque

        playerInput.isAttack = false;                 // Sinaliza o fim do ataque para o script de inputs
        playerMoviment.EnabledControls();             // habilita os controles do player
    }

    #endregion


}
