using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator anim;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        
    }


    public void SetMovementDirection(int direction)
    {
        anim.SetInteger("transition", direction);
    }



}
