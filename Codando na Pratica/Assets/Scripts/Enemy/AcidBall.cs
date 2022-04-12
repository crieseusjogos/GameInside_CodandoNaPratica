using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidBall : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == 3 || other.gameObject.layer == 6 || other.gameObject.layer == 7)
        {
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0;

            anim.Play("AcidBall_Explosion", -1);
            Destroy(gameObject, 0.59f);

        }
    }


}
