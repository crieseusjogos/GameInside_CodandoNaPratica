using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoviment : MonoBehaviour
{
    private PlayerAnimation playerAnimation;             // Armazena o Script que gerencia as Anima��es do Player

    private Rigidbody2D rig;                             // Armazena o component Rigidbody   
   
    [Header("Collision Ground Settings:")]
    [SerializeField] private bool onGround;              // Controla se est� no ch�o
    [SerializeField] private float groundRadius;         // Controla o tamanho do raio do colisor que detecta o ch�o
    [SerializeField] private Transform groundCheck;      // GameObject que passa a posi��o que ser� gerada o Circulo de colis�o (referenciado manualmente no Inspector)
    [SerializeField] private LayerMask groundLayer;      // Passa qual � a Layer do ch�o


    [Header("Moviment Settings:")]
    [SerializeField] private float speed;                // Controla a velocidade da movimenta��o do Player

    private Vector2 newMoviment;                         // Controla a for�a de movimenta��o
    private bool canControl = true;                      // Controla se pode controlar o Player


    [Header("Jump Settings:")]
    [SerializeField] private int nextJump;               // Controla se pode dar um pulo extra
    [SerializeField] private float jumpForce;            // Controla a for�a do Pulo
    
    private bool jump;                                   // Controla se pode pular
    private int extraJump;                               // Controla o n�mero de pulos extras
     
 

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();                     // Referencia o componente RigidBody
        playerAnimation = GetComponent<PlayerAnimation>();     // Referencia o Script respons�vel pelas anima��es do Player

    }

    // Start is called before the first frame update
    void Start()
    {
        extraJump = nextJump;   // Passa o n�mero de pulo totais, para a variavel que controla se pode dar mais um pulo
    }

    // Update is called once per frame
    void Update()
    {
        if (!canControl)       // Caso n�o possa controlar retorna
            return;

        ResetExtraJump();     // Fun��o que reseta os n�meros de pulos
        SetJumpAnim();        // Fun��o que gerencia a anima��o de pulo
    }

    private void FixedUpdate()
    {
        if (!canControl)       // Caso n�o possa controlar retorna
            return;

        Movement();            // Fun��o que gerencia a movimenta��o
        CheckGround();         // Fun��o que verifica se est� no ch�o
        Jump();                // Fun��o que gerencia a l�gica de pulo
    }


    #region Moviment

    public void OnMove(float direction)  // Fun��o que faz a l�gica de movimenta��o
    {
        float currentSpeed = speed;

        newMoviment = new Vector2(direction * currentSpeed, rig.velocity.y);

        if(onGround)   // Chama a fun��o que executa a anima��o de movimenta��o apenas se estiver no ch�o
        {
            playerAnimation.SetMovementDirection((int)Mathf.Abs(direction)); // Passa um valor absoluto se for um ou menos um, sempre retornando 0 parado / 1 caso se mova
        }
    
        //Flip do jogador
        Vector3 theScale = transform.localScale;                           // Armazena o transform local do player
        theScale.x = direction != 0 ? direction : transform.localScale.x;  // Verifica se o direction � diferente de 0 para executar o flip
        transform.localScale = theScale;                                   // Executa o flip
         
    }



    void Movement()    // Fun��o que faz o movimento
    {
        rig.velocity = newMoviment; // Passa o valor do novo movimento para o RigidBody
    }


    public void ResetVelocity()  // Fun��o que zera a velocidade do RigidBody do Player
    {
        rig.velocity = Vector2.zero;
    }

    #endregion


    #region Jump

    void CheckGround() // Fun��o que verifica se est� no ch�o
    {
        onGround = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);
    }

    private void OnDrawGizmos() // Fun��o que cria um Gizmo do Circulo de colis�o na Unity
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }


    void Jump() // Fun��o que executa o pulo
    {
        if (jump) // Verifica se pode pular
        {
            jump = false;

            ResetVelocity(); // zera a velocidade do pulo a cada novo pulo

            rig.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); // Adiciona a for�a do pulo

        }
    }


    void SetJumpAnim() // Fun��o que chama a anima��o do pulo
    {
        if (!onGround) // Verifica se n�o est� no ch�o
        {
            if (rig.velocity.y > 0)
            {
                playerAnimation.SetMovementDirection(2);
            }
            else if (rig.velocity.y < 0)
            {
                playerAnimation.SetMovementDirection(3);
            }
        }
     
    }

    public void SetJump() // Controla os n�meros de pulo
    {
        if(onGround || extraJump > 0)  // Verifica se est� no ch�ao ou tem pulos extras
        {
            jump = true;
            extraJump -= 1;
        }  
    }

    void ResetExtraJump() // Reseta o n�mero de pulos extras do Player
    {
        playerAnimation.SetOnGround(onGround); // Passa constantemente se o player et� no ch�o ou n�o para o animator

        if(onGround) 
        {
            extraJump = nextJump;
        }
    }

    #endregion


    #region Enabled / Disbled Controls

    public void EnabledControls() // Habilita os controles do Player
    {
        canControl = true;
    }

    public void DisableControls() // Desabilita os controles do Player
    {
        canControl = false;

        if(onGround)
        {
            ResetVelocity();  // Zera a velocidade do RigidBosy do Player
        }
    }

    #endregion


}
