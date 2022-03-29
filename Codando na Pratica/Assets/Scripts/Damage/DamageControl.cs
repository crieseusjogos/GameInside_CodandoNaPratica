using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DamageControl : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Color damageColor;

    private Color startColor;

    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;
    [SerializeField] private float invincibleTime;


    private bool invincible;
    private bool isDead;

    public UnityEvent OnDamage;
    public UnityEvent OnExitDamage;
    public UnityEvent OnDeath;


    PlayerAnimation playerAnimation;

    private void Awake()
    {
       if(GetComponent<SpriteRenderer>() != null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();    
        }
       else if(GetComponentInChildren<SpriteRenderer>() != null)
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }


       if(gameObject.name == "Player")
        {
            playerAnimation = GetComponent<PlayerAnimation>();
        }
     
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        startColor = spriteRenderer.color;
    }



    public void TakeDamage(int currentDamage)
    {
       if (isDead || invincible)
            return;

        currentHealth -= currentDamage;

        if(currentHealth <= 0)
        {
            //Morreu
            isDead = true;
            OnDeath.Invoke();
            StopAllCoroutines();
            return;
        }

        invincible = true;
        OnDamage.Invoke();
       Invoke("FinishInvincible", invincibleTime);
    }

    void FinishInvincible()
    {
        invincible = false;
    }



    public void StartInvincibleTransparence()
    {
        StartCoroutine(ShowInvincibleTransparence());
    }

    IEnumerator ShowInvincibleTransparence()
    {
        if(gameObject.name == "Player")
        {
            playerAnimation.SetHit();
            yield return new WaitForSeconds(0.3f);
            OnExitDamage.Invoke();
        }


        float timer = 0;

        while(timer < invincibleTime)
        {
            spriteRenderer.color = damageColor;
            yield return new WaitForSeconds(0.1f);
            timer += 0.1f;
            spriteRenderer.color = startColor;

            yield return new WaitForSeconds(0.1f);
            timer += 0.1f;
        }

        Debug.Log("leu");
        spriteRenderer.color = startColor;
    }


}
