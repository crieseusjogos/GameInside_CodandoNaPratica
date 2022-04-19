using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloatedActions : MonoBehaviour

{

    private BloatedMove move;              //Script de movimenta��o
    private BloatedAttack attacks;         //Script de ataque
    private BloatedAnimations anim;        //Script de anima��o

    private BoxCollider2D boxCollider;     // Colisor 

    [Header("Raycast Settings:")]
    [SerializeField] private Transform rayPoint; // Raio para a vis�o do inimigo
    [SerializeField] private float rayDistance;  // Tamanho do raio

    private bool _isAttacking;            // Controla se est� atacando
    private bool _finishMoviment;         // Controla se terminou a movimenta��o

    private bool playerIsDead;            // Controla se o player est� morto
    private bool onHit;                   // Est� tomando dano
    private bool isDead;                  // Est� morto


    #region Encapsulamento
    public bool isAttacking { get => _isAttacking; set => _isAttacking = value; }
    public bool finishMoviment { get => _finishMoviment; set => _finishMoviment = value; }


    #endregion

    private void Awake()
    {
        move = GetComponent<BloatedMove>();
        attacks = GetComponent<BloatedAttack>();
        anim = GetComponent<BloatedAnimations>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        move.canGetTarget = true;
        finishMoviment = false;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerIsDead || onHit || isDead)
            return;

        DetectPlayer();
    }


    #region Detectar o Player

    void DetectPlayer()  // Fun��o para detectar o Player, calcular sua dist�ncia e chamar o ataque 
    {
        if (isAttacking || playerIsDead || onHit || isDead)
            return;

        RaycastHit2D hit = Physics2D.Raycast(rayPoint.position, Vector2.left * transform.localScale.x, rayDistance);

        if(hit.collider != null)
        {
            if(hit.transform.CompareTag("Player")) // verifica se o objeto da colis�o � o Player
            {
                DamageControl damageControl = hit.transform.GetComponent<DamageControl>(); // Cria a linha de vis�o

                if(damageControl.isDead) // retorna caso o Player esteja morto
                {
                    playerIsDead = true;
                }

                if (playerIsDead)
                    return;

                // Calcula a dist�ncia entre o ponto de colis�o do Player na linha e o inimigo
                float playerDistance = transform.position.x - hit.point.x; 

                if(Mathf.Abs(playerDistance) <= 1f)  // Chama o ataque Corpo a corpo
                {
                    isAttacking = true;
                  
                    move.FinishMove();               // Finalizando as fun��es de movimenta��o
                    move.FinishLookingForPlayer();   // Finalizando as fun��es de movimenta��o
                    move.isAttack = false;           // Finalizando as fun��es de movimenta��o

                    attacks.Meleeattack();           // Chama fun��o de Ataque Corpo a Corpo

                }
                else if(Mathf.Abs(playerDistance) > 1f)  // Chama o ataque a Dist�ncia
                {
                    isAttacking = true;

                    move.FinishMove();                  // Finalizando as fun��es de movimenta��o
                    move.FinishLookingForPlayer();      // Finalizando as fun��es de movimenta��o
                    move.isAttack = false;              // Finalizando as fun��es de movimenta��o


                    int randomAttack = Random.Range(0, 100);      // Seleciona um n�mero Rand�mico para um ataque a Dist�ncia

                    if(randomAttack <= 50)
                    {
                        attacks.RangedAttack1();              // Ataque 1
                    }
                    else if(randomAttack > 50)
                    {
                        attacks.RangedAttack2();              // Ataque 2
                    }
                }
            }
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(rayPoint.position, Vector2.left * transform.localScale.x * rayDistance);
    }

    #endregion


    #region Enemy Actions

    public void Actions() // Verifica se n�o est� atacando para chamar as fun��es de movimenta��o
    {
        if (onHit || isDead)
            return;

        if(finishMoviment || !isAttacking)
        {
            finishMoviment = false;
            Invoke("GoNewMove", 1f);
        }

    }

    void GoNewMove() // Chama um novo movimento
    {
        if (isAttacking || onHit || isDead)
            return;

        move.StartLookingForPlayer(); // Chama a fun��o de procurar o player ao redor
    }


    #endregion

    #region Hit and Death

    //Estas fun��es s�o chamadas pelo Script DamageControl atrav�s dos Events 

    public void OnHit()  // Tomando o Dano 
    {
        onHit = true;

        move.FinishMove();                //Finalizando fun��es de movimenta��o
        move.FinishLookingForPlayer();    //Finalizando fun��es de movimenta��o

        attacks.onHit = true;             // Travando os ataques  
        move.onHit = true;                // Travando a movimenta��o    

        anim.SetHit();                    // Executa a anima��o

        Invoke("ExitHit", 0.59f);         // Chama o fim da fun��o Hit
    }


    void ExitHit()  // Finaliza a fun��o Hit
    {
        onHit = false;                  
        attacks.onHit = false;
        move.onHit = false;

        Invoke("Actions", 0.3f);
    }


    public void OnDeath()   // Fun��o de Morte
    {
        isDead = true;
        boxCollider.enabled = false;         // Desativa a colis�o

        move.FinishMove();                   // Finalizando fun��es de movimenta��o
        move.FinishLookingForPlayer();       // Finalizando fun��es de movimenta��o

        attacks.isDead = true;               // Travando os ataques  
        move.isDead = true;                  // Travando a movimenta��o

        anim.SetDeath();                     // Anima��o de Morte
    }

    #endregion

}
