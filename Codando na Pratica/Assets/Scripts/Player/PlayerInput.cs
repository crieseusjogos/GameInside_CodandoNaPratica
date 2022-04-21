using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class PlayerInput : MonoBehaviour
{
    private PlayerMoviment playerMoviment;         // Armazena o Script que gerencia os Movimentos do Player
    private PlayerAttack playerAttack;             // Armazena o Script que gerencia os Ataques do Player

    private bool _isAttack;                       // Controla se est� atacando
    private bool _isDead;                         // Controla se est� morto



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

        // Chamado em fun��es para organizar melhor o Update

        GetMovimentInput();  // Inputs para movimenta��o
        GetJumpInput();      // Inputs para pulo
        GetAttackInput();    // Inputs para o ataque
    }


    #region Get Inputs


    // Movimenta��o

    void GetMovimentInput() // Fun��o que Captura os inputs de Movimenta��o
    {
        if (isAttack || isDead) // caso esteja atacando ou morto retorna
            return;

        playerMoviment.OnMove(Input.GetAxisRaw("Horizontal"));     // Passa para a fun��o de movimenta��o o Input do eixo Horizaontal
    }



    // Pulo
    void GetJumpInput() // Fun��o que Captura os inputs para o Pulo
    {
        if( Input.GetButtonDown("Jump"))  // Verifica se o bot�o Jump foi pressionado
        {
            playerMoviment.SetJump();     // Chama a fun��o que inicia a l�gica de pulo
        }
    }



    // Ataque
    void GetAttackInput() // Fun��o que Captura os inputs para o Ataque
    {
        if(Input.GetMouseButtonDown(0)) // Verifica se o bot�o Esquerdo do Mouse foi pressionado
        {
            playerAttack.SetAttack();  // Chama a fun��o que inicia a l�gica de ataque
        }
    }

    #endregion


}
