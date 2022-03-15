using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoviment : MonoBehaviour
{
    private PlayerAnimation playerAnimation;

    private Rigidbody2D rig;
    private Vector2 newMoviment;

   // private bool facingRight = true;

    [SerializeField] private bool onGround;
    [SerializeField] private float groundRadius;

    private bool jump;
    private bool doubleJump;
    private int extraJump;



   [SerializeField] private int nextJump;
   

    [SerializeField] private float jumpForce;
  

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;


    [Header("Moviment:")]
    [SerializeField] private float speed;
   

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<PlayerAnimation>();

    }

    // Start is called before the first frame update
    void Start()
    {
        extraJump = nextJump;
    }

    // Update is called once per frame
    void Update()
    {
        ResetExtraJump();
        SetJumpAnim();
    }

    private void FixedUpdate()
    {
        Movement();
        CheckGround();
        Jump();
    }


    #region Moviment

    public void OnMove(float direction)
    {
        float currentSpeed = speed;

        newMoviment = new Vector2(direction * currentSpeed, rig.velocity.y);

        if(onGround)
        {
            playerAnimation.SetMovementDirection((int)Mathf.Abs(direction));
        }
    

        //Flip do Player
        Vector3 theScale = transform.localScale;
        theScale.x = direction != 0 ? direction : transform.localScale.x;
        transform.localScale = theScale;

    }



    void Movement()
    {
        rig.velocity = newMoviment;
    }

    #endregion



    void CheckGround()
    {
        onGround = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }


    void Jump()
    {
        if(jump)
        {
            jump = false;

            rig.velocity = Vector2.zero;
            rig.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        }
    }


    void SetJumpAnim()
    {
        if(!onGround)
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
    public void SetJump()
    {
        if(onGround || extraJump > 0)
        {
            jump = true;
            extraJump -= 1;
        }  
    }

    void ResetExtraJump()
    {
        if(onGround)
        {
            extraJump = nextJump;
        }
    }


}
