using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloatedMove : MonoBehaviour
{

    private Rigidbody2D rb;
    private BloatedAnimations anim;
    private BloatedActions actions;

    [SerializeField] private List<Transform> pointsMove = new List<Transform>();
    [SerializeField] private bool canMove;
    [SerializeField] private float speed;

    private Transform targetPosition;
    private bool findTarget;
    private int moveState;
    private int moveAnim;

    private bool right;

    [SerializeField] private bool _canGetTarget;

    [SerializeField] private bool _isAttack;

    private bool _onHit;
    private bool _isDead;

 

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
        right = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttack || onHit || isDead)
            return;

        GoNewMovement();
        SetMovimentAnim(moveAnim);
    }


    private void FixedUpdate()
    {
        if (isAttack || onHit || isDead)
            return;


        Move(moveState);
        CheckTargetPosition();
    }

    public void GoNewMovement()
    {

        if(canGetTarget)
        {
            canGetTarget = false;

            targetPosition = pointsMove[Random.Range(0, pointsMove.Count)];
            findTarget = true;
        }
    }



    void CheckTargetPosition()
    {
       if(findTarget)
        {

           if(targetPosition.position.x - transform.position.x > 0.02f)
            {
                //Direita
                moveState = 1;
                moveAnim = 1;
                canMove = true;

                right = true;
                Flip(false);
            }
           else if(targetPosition.position.x - transform.position.x < -0.02)
            {
                // Esquerda
                moveState = 2;
                moveAnim = 1;
                canMove = true;

                right = false;
                Flip(false);
            }
           else if(targetPosition.position.x - transform.position.x <= 0.02f && targetPosition.position.x - transform.position.x >= -0.02f)
            {
                //Parar
                moveState = 0;
                moveAnim = 0;

                rb.velocity = Vector2.zero;
                Invoke("FinishMove", 0.01f);

            }

        }
    }


    private void Move(int state)
    {

        if (state == 0)
            return;

        if(canMove)
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


    public void FinishMove()
    {
        moveState = 0;
        moveAnim = 0;
        canMove = false;
        findTarget = false;
        canGetTarget = false;

        rb.velocity = Vector2.zero;

        actions.finishMoviment = true;
        actions.Actions();
    }



    void Flip(bool onLooking)
    {

        if(!onLooking)
        {
            Vector3 theScale = transform.localScale;
            theScale.x = right ? -1 : 1;
            transform.localScale = theScale;

        }
        else if(onLooking)
        {
            int left = -1;
            int right = 1;
            int randoDirection = Random.Range(0, 100);

            Vector3 theScale = transform.localScale;
            theScale.x = randoDirection <= 50 ? left : right;
            transform.localScale = theScale;

        }


    }


    public void StartLookingForPlayer()
    {
        StartCoroutine("LookingForThePlayer", 0.1f);
    }

    public void FinishLookingForPlayer()
    {
        StopCoroutine("LookingForThePlayer");
    }



    IEnumerator LookingForThePlayer()
    {
        Flip(true);

        yield return new WaitForSeconds(Random.Range(0.3f, 1.5f));

        Flip(true);

        yield return new WaitForSeconds(Random.Range(0.3f, 1.5f));

        Flip(true);

        yield return new WaitForSeconds(Random.Range(0.3f, 1.5f));

        canGetTarget = true;
    }









    void SetMovimentAnim(int moviment)
    {
        anim.SetMovimentAnim(moviment);
    }




}
