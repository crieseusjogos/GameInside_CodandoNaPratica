using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpike : MonoBehaviour
{
    private Animator anim;


    private void Awake()
    {
        anim = GetComponent<Animator>();
    }


    private void OnEnable()   //Dá Play na animação ao ser ativado
    {
        anim.Play("Spike_Attack", -1);
    }


}
