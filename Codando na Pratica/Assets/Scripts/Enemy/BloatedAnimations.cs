using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloatedAnimations : MonoBehaviour
{
    [SerializeField] private Animator _anim;


    public Animator Anim { get => _anim; set => _anim = value; }

    private void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
    }


    public void SetMovimentAnim(int moviment)
    {
        Anim.SetInteger("transition", moviment);
    }

}
