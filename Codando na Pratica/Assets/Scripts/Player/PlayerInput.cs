using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInput : MonoBehaviour
{
    private PlayerMoviment playerMoviment;
    private PlayerAttack playerAttack;

    private bool _isAttack;
    private bool _isDead;

    public bool isAttack { get => _isAttack; set => _isAttack = value; }
    public bool isDead { get => _isDead; set => _isDead = value; }

    private void Awake()
    {
        playerMoviment = GetComponent<PlayerMoviment>();
        playerAttack = GetComponent<PlayerAttack>();
    } 

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        GetMovimentInput();
        GetJumpInput();
        GetAttackInput();
    }
 



    void GetMovimentInput()
    {
        if (isAttack || isDead)
            return;

        playerMoviment.OnMove(Input.GetAxisRaw("Horizontal"));
    }

    void GetJumpInput()
    {
        if( Input.GetButtonDown("Jump"))
        {
            playerMoviment.SetJump();
        }
    }

    void GetAttackInput()
    {
        if(Input.GetMouseButtonDown(0))
        {
            playerAttack.SetAttack();
        }
    }




}
