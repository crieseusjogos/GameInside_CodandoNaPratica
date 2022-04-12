using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloatedAttack : MonoBehaviour
{

    private BloatedAnimations anim;
    private BloatedMove move;
    private BloatedActions actions;


    [SerializeField] private Rigidbody2D acidBullet;
    [SerializeField] private Transform acidSpwanPosition;


    [SerializeField] private Transform groundSpikeSpawm;
    [SerializeField] private GameObject groundSpike;


    private bool _onHit;
    private bool _isDead;

    public bool onHit { get => _onHit; set => _onHit = value; }
    public bool isDead { get => _isDead; set => _isDead = value; }

    private void Awake()
    {
        anim = GetComponent<BloatedAnimations>();
        move = GetComponent<BloatedMove>();
        actions = GetComponent<BloatedActions>();
    }




    public void Meleeattack()
    {

        if (onHit || isDead)
            return;

        anim.SetBoolIsAttacking(true);
        anim.SetMeleeAttack();
       

        Invoke("FinishAttack", 0.59f);

    }



public void RangedAttack1()
    {
        if (onHit || isDead)
            return;

        anim.SetRangedAttack1();
        anim.SetBoolIsAttacking(true);

        Invoke("InstanceAcidBullet", 0.2f);
        Invoke("FinishAttack", 3f);

    }



    public void RangedAttack2()
    {
        if (onHit || isDead)
            return;

        anim.SetRangedAttack2();
        anim.SetBoolIsAttacking(true);

        Invoke("InstnceGroundSpikes", 0.2f);
        Invoke("FinishAttack", 3f);

    }









    private void  FinishAttack()
    {
        move.isAttack = false;

        anim.SetBoolIsAttacking(false);

        actions.isAttacking = false;
        actions.Actions();

    }





    void InstanceAcidBullet()
    {
        if (onHit || isDead)
            return;


        if (transform.localScale.x == 1)
        {
            Vector2 acidBulletImpulse = new Vector2(-5, 8);

            Rigidbody2D newAcidBullet = Instantiate(acidBullet, acidSpwanPosition.position, transform.rotation);

            newAcidBullet.AddForce(acidBulletImpulse, ForceMode2D.Impulse);
        }
        else if(transform.localScale.x == -1)
        {
            Vector2 acidBulletImpulse = new Vector2(5, 8);

            Rigidbody2D newAcidBullet = Instantiate(acidBullet, acidSpwanPosition.position, transform.rotation);

            newAcidBullet.AddForce(acidBulletImpulse, ForceMode2D.Impulse);
        }


    }



    void InstnceGroundSpikes()
    {
        if (onHit || isDead)
            return;

        Instantiate(groundSpike, groundSpikeSpawm.position, transform.rotation);
    }



}
