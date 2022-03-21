using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAttack : MonoBehaviour
{
    private PlayerAnimation playerAnimation;
    private PlayerMoviment playerMoviment;
    private PlayerInput playerInput;


    private bool canAttack = true;

    private void Awake()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
        playerMoviment = GetComponent<PlayerMoviment>();
        playerInput = GetComponent<PlayerInput>();

      
    }


    public void Attack()
    {
        if(canAttack)
        {
            canAttack = false;

            playerInput.isAttack = true;
            playerMoviment.DisableControls();

            int randomAttack = Random.Range(0, 100);

            playerAnimation.SetAttack(randomAttack);
            Invoke("CanNewAttack", 0.7f)
;        }

    }


    void CanNewAttack()
    {
        canAttack = true;

        playerInput.isAttack = false;
        playerMoviment.EnabledControls();
    }

}
