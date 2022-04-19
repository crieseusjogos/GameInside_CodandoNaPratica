using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DamageControl : MonoBehaviour
{
    [Header("Select Damage Color:")]
    [SerializeField] private Color damageColor;

    private SpriteRenderer spriteRenderer;
    private Color startColor;

    [Header("Health Settings:")]
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;
    [SerializeField] private float invincibleTime;


    private bool invincible;
    private bool _isDead;

    [Header("Envents Settings:")]
    public UnityEvent OnDamage;
    public UnityEvent OnExitDamage;
    public UnityEvent OnDeath;


    PlayerAnimation playerAnimation;

    #region Encapsuloamento

    public bool isDead { get => _isDead; set => _isDead = value; }

    #endregion

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


    #region Take Damage

    public void TakeDamage(int currentDamage) // Fun��o que recebe o dano
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

    void FinishInvincible() // Fun��o finaliza a Invencibilidade
    {
        invincible = false;
    }

    #endregion


    #region Invincible

    public void StartInvincibleTransparence() // Fun��o que inicia a corrotina de invencibilidade
    {
        StartCoroutine(ShowInvincibleTransparence());
    }

    IEnumerator ShowInvincibleTransparence()  // faz a Invencibilidade e executa o feedback no Sprite
    {
        if(gameObject.name == "Player")
        {
            playerAnimation.SetHit();
            yield return new WaitForSeconds(0.42f);
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

        spriteRenderer.color = startColor;
    }
#endregion

}
