using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoviment : MonoBehaviour
{
    private PlayerAnimation playerAnimation;             // Armazena o Script que gerencia as Animações do Player

    private Rigidbody2D rig;                             // Armazena o component Rigidbody   
   
    [Header("Collision Ground Settings:")]
    [SerializeField] private bool onGround;              // Controla se está no chão
    [SerializeField] private float groundRadius;         // Controla o tamanho do raio do colisor que detecta o chão
    [SerializeField] private Transform groundCheck;      // GameObject que passa a posição que será gerada o Circulo de colisão (referenciado manualmente no Inspector)
    [SerializeField] private LayerMask groundLayer;      // Passa qual é a Layer do chão


    [Header("Moviment Settings:")]
    [SerializeField] private float speed;                // Controla a velocidade da movimentação do Player

    private Vector2 newMoviment;                         // Controla a força de movimentação
    private bool canControl = true;                      // Controla se pode controlar o Player


    [Header("Jump Settings:")]
    [SerializeField] private int nextJump;               // Controla se pode dar um pulo extra
    [SerializeField] private float jumpForce;            // Controla a força do Pulo
    
    private bool jump;                                   // Controla se pode pular
    private int extraJump;                               // Controla o número de pulos extras
     
 

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();                     // Referencia o componente RigidBody
        playerAnimation = GetComponent<PlayerAnimation>();     // Referencia o Script responsável pelas animações do Player

    }

    // Start is called before the first frame update
    void Start()
    {
        extraJump = nextJump;   // Passa o número de pulo totais, para a variavel que controla se pode dar mais um pulo
    }

    // Update is called once per frame
    void Update()
    {
        if (!canControl)       // Caso não possa controlar retorna
            return;

        ResetExtraJump();     // Função que reseta os números de pulos
        SetJumpAnim();        // Função que gerencia a animação de pulo
    }

    private void FixedUpdate()
    {
        if (!canControl)       // Caso não possa controlar retorna
            return;

        Movement();            // Função que gerencia a movimentação
        CheckGround();         // Função que verifica se está no chão
        Jump();                // Função que gerencia a lógica de pulo
    }


    #region Moviment

    public void OnMove(float direction)  // Função que faz a lógica de movimentação
    {
        float currentSpeed = speed;

        newMoviment = new Vector2(direction * currentSpeed, rig.velocity.y);

        if(onGround)   // Chama a função que executa a animação de movimentação apenas se estiver no chão
        {
            playerAnimation.SetMovementDirection((int)Mathf.Abs(direction)); // Passa um valor absoluto se for um ou menos um, sempre retornando 0 parado / 1 caso se mova
        }
    
        //Flip do jogador
        Vector3 theScale = transform.localScale;                           // Armazena o transform local do player
        theScale.x = direction != 0 ? direction : transform.localScale.x;  // Verifica se o direction é diferente de 0 para executar o flip
        transform.localScale = theScale;                                   // Executa o flip
         
    }



    void Movement()    // Função que faz o movimento
    {
        rig.velocity = newMoviment; // Passa o valor do novo movimento para o RigidBody
    }


    public void ResetVelocity()  // Função que zera a velocidade do RigidBody do Player
    {
        rig.velocity = Vector2.zero;
    }

    #endregion


    #region Jump

    void CheckGround() // Função que verifica se está no chão
    {
        onGround = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);
    }

    private void OnDrawGizmos() // Função que cria um Gizmo do Circulo de colisão na Unity
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }


    void Jump() // Função que executa o pulo
    {
        if (jump) // Verifica se pode pular
        {
            jump = false;

            ResetVelocity(); // zera a velocidade do pulo a cada novo pulo

            rig.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); // Adiciona a força do pulo

        }
    }


    void SetJumpAnim() // Função que chama a animação do pulo
    {
        if (!onGround) // Verifica se não está no chão
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

    public void SetJump() // Controla os números de pulo
    {
        if(onGround || extraJump > 0)  // Verifica se está no chçao ou tem pulos extras
        {
            jump = true;
            extraJump -= 1;
        }  
    }

    void ResetExtraJump() // Reseta o número de pulos extras do Player
    {
        playerAnimation.SetOnGround(onGround); // Passa constantemente se o player etá no chão ou não para o animator

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
