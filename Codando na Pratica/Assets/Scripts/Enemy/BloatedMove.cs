using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloatedMove : MonoBehaviour
{

    private Rigidbody2D rb; 

    private BloatedAnimations anim;       // Script de Anima��o
    private BloatedActions actions;       // Script de A��es

    [Header("Moviment Settings:")]
    [SerializeField] private List<Transform> pointsMove = new List<Transform>();    // Lista de pontos de moviment��o 
    [SerializeField] private float speed;                                           // Velocidade da movimenta��o


    private Transform targetPosition;    // Ponto alvo para se mover
    private bool canMove;                // Pode se mover
    private bool findTarget;             // Procurar um Alvo
    private bool right;                  // Se est� virado para a direita
    private int moveState;               // Controla se est� se movendo ou parado
     private int moveAnim;                // Passa um valor para o Integger de transi��o de anima��o
    

    private bool _canGetTarget;      // Pode buscar um novo alvo para se movimentar
    private bool _isAttack;          // Est� atacando
    private bool _onHit;             // Tomou Dano
    private bool _isDead;            // Est� Morto 

 

    #region Encapsulamento

    public bool canGetTarget { get => _canGetTarget; set => _canGetTarget = value; }
    public bool isAttack { get => _isAttack; set => _isAttack = value; }
    public bool onHit { get => _onHit; set => _onHit = value; }
    public bool isDead { get => _isDead; set => _isDead = value; }

    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<BloatedAnimations>();
        actions = GetComponent<BloatedActions>();
    }

    // Start is called before the first frame update
    void Start()
    {
        right = false;  // Passar para false pois o sprite come�a virado para a esquerda
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttack || onHit || isDead)
            return;

        GoNewMovement();              //Fun��o que seleciona um Alvo 
        SetMovimentAnim(moveAnim);    // Seta as anima��es com um parametro int
    }


    private void FixedUpdate()
    {
        if (isAttack || onHit || isDead)
            return;

        Move(moveState);         // Seta a dire��o de movimenta��o com um parametro int
        CheckTargetPosition();   // Calcula a dist�ncia entre o inimigo e o ponto Alvo para se mover
    }

    #region Moviment

    public void GoNewMovement()  // Seleciona um novo alvo para se mover
    {
        if(canGetTarget)
        {
            canGetTarget = false;

            targetPosition = pointsMove[Random.Range(0, pointsMove.Count)];
            findTarget = true;
        }
    }



    void CheckTargetPosition() // Calcula constantemente a dist�ncia entre o inimigo e o Ponto Alvo
    {
       if(findTarget)  // verifica se pode buscar um alvo
        {

           if(targetPosition.position.x - transform.position.x > 0.02f)    //Direita
            {  
                moveState = 1;         // Est� se movendo recebe 1
                moveAnim = 1;          // Valor do transition de anima��o
                canMove = true;        // Pode se mover

                right = true;          // Virado para direita
                Flip(false);           // Faz o flip no sprite do inimigo
            }
           else if(targetPosition.position.x - transform.position.x < -0.02)    // Esquerda
            {         
                moveState = 2;         // Est� se movendo recebe 1
                moveAnim = 1;          // Valor do transition de anima��o
                canMove = true;        // Pode se mover

                right = false;         // Virado para a Esquerda
                Flip(false);           // Faz o flip no sprite do inimigo, passa false para a bool onLooking
            }
           else if(targetPosition.position.x - transform.position.x <= 0.02f && targetPosition.position.x - transform.position.x >= -0.02f)   // Chegou no alvo
            {
               
                moveState = 0;          // Est� se movendo recebe o
                moveAnim = 0;           // Valor do transition de anima��o

                rb.velocity = Vector2.zero;          // zera a velocidade do inimigo
                Invoke("FinishMove", 0.01f);         // Finaliza o movimento, passa false para a bool onLooking

            }

        }
    }


    private void Move(int state) // Passa a for�a e a dire��o do movimento
    {
        if (state == 0)
            return;

        if(canMove)  // Verifica se pode se mover
        {
            if (state == 1)
            {
                rb.velocity = Vector2.right * speed;
            }
            else if (state == 2)
            {
                rb.velocity = Vector2.left * speed;
            }
        }
    }


    public void FinishMove()  // Finaliza a moviment���o
    {
        moveState = 0;            
        moveAnim = 0;
        canMove = false;
        findTarget = false;
        canGetTarget = false;

        rb.velocity = Vector2.zero;

        actions.finishMoviment = true;      // Passa o fim da movimenta��o para o Controlador de a��es
        actions.Actions();                  // Chama uma nova a��o
    }

    #endregion


    #region Flip

    void Flip(bool onLooking)    // Faz o Flip no inimigo
    {
        if(!onLooking)           // faz o flip padr�o
        {
            Vector3 theScale = transform.localScale;          // Armazena a pois��o atual
            theScale.x = right ? -1 : 1;                      // verifica a dire��o em que est� olhando
            transform.localScale = theScale;                  // faz o Flip

        }
        else if(onLooking)      // Faz o flip da olhar ao redor buscando o Player
        {
            int left = -1;                             
            int right = 1;
            int randoDirection = Random.Range(0, 100);  // define um valor randomico para olhar

            Vector3 theScale = transform.localScale;                // Armazena a pois��o atual
            theScale.x = randoDirection <= 50 ? left : right;       // verifica a dire��o em que est� olhando
            transform.localScale = theScale;                        // faz o Flip

        }
    }

    #endregion


    #region Looking For Player

    public void StartLookingForPlayer()      // Inicia a corrotina de para olhar ao redor
    {
        StartCoroutine("LookingForThePlayer", 0.1f);
    }

    public void FinishLookingForPlayer()     // Finaliza a corrotina de olhar ao redor 
    {
        StopCoroutine("LookingForThePlayer");
    }



    IEnumerator LookingForThePlayer()       // Coortina qeu faz o inimigo olhar ao redor buscando o Player
    {
        Flip(true);

        yield return new WaitForSeconds(Random.Range(0.3f, 1.5f));

        Flip(true);

        yield return new WaitForSeconds(Random.Range(0.3f, 1.5f));

        Flip(true);

        yield return new WaitForSeconds(Random.Range(0.3f, 1.5f));

        canGetTarget = true;               // Reinicia o ciclo de andar
    }



    #endregion


    #region Set Animation

    void SetMovimentAnim(int moviment)
    {
        anim.SetMovimentAnim(moviment);
    }

    #endregion


}
