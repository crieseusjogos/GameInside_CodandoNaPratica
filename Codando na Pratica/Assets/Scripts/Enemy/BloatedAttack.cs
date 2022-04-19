using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloatedAttack : MonoBehaviour
{

    private BloatedAnimations anim;                         // Script de animação
    private BloatedMove move;                               // Script de movimentação
    private BloatedActions actions;                         // Script de Ações


    [Header("Acid Bullet Settings:")]
    [SerializeField] private Rigidbody2D acidBullet;        // Prefab da Bola Acida
    [SerializeField] private Transform acidSpwanPosition;   // Ponto de Spawn

    [Header("Spikes Attack Settings:")]
    [SerializeField] private GameObject groundSpike;        // Prefab dos Espinhos
    [SerializeField] private Transform groundSpikeSpawm;    // Ponto de Spawn

    private bool _onHit;                                    // Tomando Dano
    private bool _isDead;                                   // Está Morto


    #region Encapsulamento
    public bool onHit { get => _onHit; set => _onHit = value; }
    public bool isDead { get => _isDead; set => _isDead = value; }

    #endregion

    private void Awake()
    {
        anim = GetComponent<BloatedAnimations>();
        move = GetComponent<BloatedMove>();
        actions = GetComponent<BloatedActions>();
    }

    #region Melee Attack

    public void Meleeattack()   // Executa o Ataque Corpo a corpo
    {
        if (onHit || isDead)
            return;

        anim.SetBoolIsAttacking(true);
        anim.SetMeleeAttack();
       
        Invoke("FinishAttack", 0.59f);  // Finaliza o Ataque

    }

    #endregion

    #region Ranged Attacks

    public void RangedAttack1()   // Executa o Ataque Acido
    {
        if (onHit || isDead)
            return;

        anim.SetRangedAttack1();
        anim.SetBoolIsAttacking(true);

        Invoke("InstanceAcidBullet", 0.2f);  // Instancia a bola Acida
        Invoke("FinishAttack", 3f);          // Finaliza o Ataque

    }

    public void RangedAttack2()    // Execeuta o Ataque de Espinhos
    {
        if (onHit || isDead)
            return;

        anim.SetRangedAttack2();
        anim.SetBoolIsAttacking(true);

        Invoke("InstnceGroundSpikes", 0.2f); // Istancia os Espinhos
        Invoke("FinishAttack", 3f);          // Finaliza o Ataque

    }


    #endregion

    #region Finish Attack


    private void  FinishAttack() //Finaliza os Ataques
    {
        move.isAttack = false;

        anim.SetBoolIsAttacking(false);

        actions.isAttacking = false;       
        actions.Actions();               // Chama uma nova Ação

    }

    #endregion


    #region Instance Prefabs
    void InstanceAcidBullet()   // Instancia a Bola Acida
    {
        if (onHit || isDead)
            return;

        if (transform.localScale.x == 1)     // Verifica se está para a esquerda
        {
            Vector2 acidBulletImpulse = new Vector2(-5, 8);    // Armazena uma força de arremesso e sua direção

            Rigidbody2D newAcidBullet = Instantiate(acidBullet, acidSpwanPosition.position, transform.rotation);  // Cria uma nova bullet em cena

            newAcidBullet.AddForce(acidBulletImpulse, ForceMode2D.Impulse);  // Adiciona a Força de arremeço na Bullet
        }
        else if(transform.localScale.x == -1)   // Verifica se está para a direita
        {
            Vector2 acidBulletImpulse = new Vector2(5, 8);   // Armazena uma força de arremesso e sua direção

            Rigidbody2D newAcidBullet = Instantiate(acidBullet, acidSpwanPosition.position, transform.rotation); // Cria uma nova bullet em cena

            newAcidBullet.AddForce(acidBulletImpulse, ForceMode2D.Impulse);   // Adiciona a Força de arremeço na Bullet
        }
    }


    void InstnceGroundSpikes() // Instancia os Espinhos
    {
        if (onHit || isDead)
            return;

        Instantiate(groundSpike, groundSpikeSpawm.position, transform.rotation);
    }

    #endregion

}
