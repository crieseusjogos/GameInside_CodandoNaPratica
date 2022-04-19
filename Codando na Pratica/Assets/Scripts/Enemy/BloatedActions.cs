using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloatedActions : MonoBehaviour

{

    private BloatedMove move;              //Script de movimentação
    private BloatedAttack attacks;         //Script de ataque
    private BloatedAnimations anim;        //Script de animação

    private BoxCollider2D boxCollider;     // Colisor 

    [Header("Raycast Settings:")]
    [SerializeField] private Transform rayPoint; // Raio para a visão do inimigo
    [SerializeField] private float rayDistance;  // Tamanho do raio

    private bool _isAttacking;            // Controla se está atacando
    private bool _finishMoviment;         // Controla se terminou a movimentação

    private bool playerIsDead;            // Controla se o player está morto
    private bool onHit;                   // Está tomando dano
    private bool isDead;                  // Está morto


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

    void DetectPlayer()  // Função para detectar o Player, calcular sua distância e chamar o ataque 
    {
        if (isAttacking || playerIsDead || onHit || isDead)
            return;

        RaycastHit2D hit = Physics2D.Raycast(rayPoint.position, Vector2.left * transform.localScale.x, rayDistance);

        if(hit.collider != null)
        {
            if(hit.transform.CompareTag("Player")) // verifica se o objeto da colisão é o Player
            {
                DamageControl damageControl = hit.transform.GetComponent<DamageControl>(); // Cria a linha de visão

                if(damageControl.isDead) // retorna caso o Player esteja morto
                {
                    playerIsDead = true;
                }

                if (playerIsDead)
                    return;

                // Calcula a distância entre o ponto de colisão do Player na linha e o inimigo
                float playerDistance = transform.position.x - hit.point.x; 

                if(Mathf.Abs(playerDistance) <= 1f)  // Chama o ataque Corpo a corpo
                {
                    isAttacking = true;
                  
                    move.FinishMove();               // Finalizando as funções de movimentação
                    move.FinishLookingForPlayer();   // Finalizando as funções de movimentação
                    move.isAttack = false;           // Finalizando as funções de movimentação

                    attacks.Meleeattack();           // Chama função de Ataque Corpo a Corpo

                }
                else if(Mathf.Abs(playerDistance) > 1f)  // Chama o ataque a Distância
                {
                    isAttacking = true;

                    move.FinishMove();                  // Finalizando as funções de movimentação
                    move.FinishLookingForPlayer();      // Finalizando as funções de movimentação
                    move.isAttack = false;              // Finalizando as funções de movimentação


                    int randomAttack = Random.Range(0, 100);      // Seleciona um número Randômico para um ataque a Distância

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

    public void Actions() // Verifica se não está atacando para chamar as funções de movimentação
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

        move.StartLookingForPlayer(); // Chama a função de procurar o player ao redor
    }


    #endregion

    #region Hit and Death

    //Estas funções são chamadas pelo Script DamageControl através dos Events 

    public void OnHit()  // Tomando o Dano 
    {
        onHit = true;

        move.FinishMove();                //Finalizando funções de movimentação
        move.FinishLookingForPlayer();    //Finalizando funções de movimentação

        attacks.onHit = true;             // Travando os ataques  
        move.onHit = true;                // Travando a movimentação    

        anim.SetHit();                    // Executa a animação

        Invoke("ExitHit", 0.59f);         // Chama o fim da função Hit
    }


    void ExitHit()  // Finaliza a função Hit
    {
        onHit = false;                  
        attacks.onHit = false;
        move.onHit = false;

        Invoke("Actions", 0.3f);
    }


    public void OnDeath()   // Função de Morte
    {
        isDead = true;
        boxCollider.enabled = false;         // Desativa a colisão

        move.FinishMove();                   // Finalizando funções de movimentação
        move.FinishLookingForPlayer();       // Finalizando funções de movimentação

        attacks.isDead = true;               // Travando os ataques  
        move.isDead = true;                  // Travando a movimentação

        anim.SetDeath();                     // Animação de Morte
    }

    #endregion

}
