using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class PlayerInput : MonoBehaviour
{
    private PlayerMoviment playerMoviment;         // Armazena o Script que gerencia os Movimentos do Player
    private PlayerAttack playerAttack;             // Armazena o Script que gerencia os Ataques do Player

    private bool _isAttack;                       // Controla se está atacando
    private bool _isDead;                         // Controla se está morto



    #region Encapsulamento

    public bool isAttack { get => _isAttack; set => _isAttack = value; }
    public bool isDead { get => _isDead; set => _isDead = value; }

    #endregion



    private void Awake()
    {
        playerMoviment = GetComponent<PlayerMoviment>();   // Referencia o Script que gerencia os Movimentos do Player
        playerAttack = GetComponent<PlayerAttack>();       // Referencia o Script que gerencia os Ataques do Player

        Application.targetFrameRate = 60;                  // Comando que tenta manter o alvo dos Fps do game em torno de 60 FPS 
    } 

 
    // Update is called once per frame
    void Update()
    {

        // Chamado em funções para organizar melhor o Update

        GetMovimentInput();  // Inputs para movimentação
        GetJumpInput();      // Inputs para pulo
        GetAttackInput();    // Inputs para o ataque
    }


    #region Get Inputs


    // Movimentação

    void GetMovimentInput() // Função que Captura os inputs de Movimentação
    {
        if (isAttack || isDead) // caso esteja atacando ou morto retorna
            return;

        playerMoviment.OnMove(Input.GetAxisRaw("Horizontal"));     // Passa para a função de movimentação o Input do eixo Horizaontal
    }



    // Pulo
    void GetJumpInput() // Função que Captura os inputs para o Pulo
    {
        if( Input.GetButtonDown("Jump"))  // Verifica se o botão Jump foi pressionado
        {
            playerMoviment.SetJump();     // Chama a função que inicia a lógica de pulo
        }
    }



    // Ataque
    void GetAttackInput() // Função que Captura os inputs para o Ataque
    {
        if(Input.GetMouseButtonDown(0)) // Verifica se o botão Esquerdo do Mouse foi pressionado
        {
            playerAttack.SetAttack();  // Chama a função que inicia a lógica de ataque
        }
    }

    #endregion


}
