using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloatedActions : MonoBehaviour

{

    private BloatedMove move;
    private BloatedAttack attacks;
    private BloatedAnimations anim;

    private BoxCollider2D boxCollider;

    [SerializeField] private Transform rayPoint;
    [SerializeField] private float rayDistance;

    [SerializeField] private bool _isAttacking;
    [SerializeField] private bool _finishMoviment;



    public bool isAttacking { get => _isAttacking; set => _isAttacking = value; }
    public bool finishMoviment { get => _finishMoviment; set => _finishMoviment = value; }

    private bool playerIsDead;
    private bool onHit;
    private bool isDead;


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



    void DetectPlayer()
    {
        if (isAttacking || playerIsDead || onHit || isDead)
            return;

        RaycastHit2D hit = Physics2D.Raycast(rayPoint.position, Vector2.left * transform.localScale.x, rayDistance);

        if(hit.collider != null)
        {
            if(hit.transform.CompareTag("Player"))
            {
                DamageControl damageControl = hit.transform.GetComponent<DamageControl>();

                if(damageControl.isDead)
                {
                    playerIsDead = true;
                }

                if (playerIsDead)
                    return;


                float playerDistance = transform.position.x - hit.point.x;
                if(Mathf.Abs(playerDistance) <= 1f)
                {
                    //Ataque Corpo a corpo
                    Debug.Log("ataque corpo a corpo");

                    isAttacking = true;
                  

                    move.FinishMove();
                    move.FinishLookingForPlayer();
                    move.isAttack = false;

                    attacks.Meleeattack();

                }
                else if(Mathf.Abs(playerDistance) > 1f)
                {
                    //Ataque a distância
                    Debug.Log("ataque a distância");
                    isAttacking = true;

                    move.FinishMove();
                    move.FinishLookingForPlayer();
                    move.isAttack = false;


                    int randomAttack = Random.Range(0, 100);
                    if(randomAttack <= 50)
                    {
                        attacks.RangedAttack1();
                    }
                    else if(randomAttack > 50)
                    {
                        attacks.RangedAttack2();
                    }

                   

                }


            }
        }


    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(rayPoint.position, Vector2.left * transform.localScale.x * rayDistance);
    }


    public void Actions()
    {
        if (onHit || isDead)
            return;

        if(finishMoviment || !isAttacking)
        {
            finishMoviment = false;
            Invoke("GoNewMove", 1f);
        }

    }

    void GoNewMove()
    {
        if (isAttacking || onHit || isDead)
            return;

        move.StartLookingForPlayer();
    }



    public void OnHit()
    {
        onHit = true;

        move.FinishMove();
        move.FinishLookingForPlayer();

        attacks.onHit = true;
        move.onHit = true;

        anim.SetHit();

        Invoke("ExitHit", 0.59f);
    }


    void ExitHit()
    {
        onHit = false;
        attacks.onHit = false;
        move.onHit = false;

        Invoke("Actions", 0.3f);
    }


    public void OnDeath()
    {
        isDead = true;
        boxCollider.enabled = false;

        move.FinishMove();
        move.FinishLookingForPlayer();

        attacks.isDead = true;
        move.isDead = true;

        anim.SetDeath();
    }


}
