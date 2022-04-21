using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DamageControl : MonoBehaviour
{
    [Header("Select Damage Color:")]
    [SerializeField] private Color damageColor;         // Passa a cor de Dano para o Sprite

    private SpriteRenderer spriteRenderer;         
    private Color startColor;                           // Armazena a cor inicial do sprite

    [Header("Health Settings:")]
    [SerializeField] private int maxHealth;             // Vida Máxima
    [SerializeField] private int currentHealth;         // Vida atual
    [SerializeField] private float invincibleTime;      // define o tempo de invencibilidade

     
    private bool invincible;                           // Controla se está invencivel
    private bool _isDead;                              // Controla se está morto

    [Header("Envents Settings:")]                      // Eventos que possibilitam personalizar as funções abaixo, e executar coisas funções diferentes
    public UnityEvent OnDamage;                        // Chamdo ao tomar o Dano
    public UnityEvent OnExitDamage;                    // Chamado ao fim da Invencibilidade
    public UnityEvent OnDeath;                         // Chamado quando morre


    PlayerAnimation playerAnimation;

    #region Encapsuloamento

    public bool isDead { get => _isDead; set => _isDead = value; }

    #endregion

    private void Awake()
    {
       if(GetComponent<SpriteRenderer>() != null) // Verifica se o componente Sprite Renderer está no objeto pai e o referencia
        {
            spriteRenderer = GetComponent<SpriteRenderer>();    
        }
       else if(GetComponentInChildren<SpriteRenderer>() != null)  // Verifica se o component Sprite Renderer está no objeto filho e o referencia
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }


       if(gameObject.name == "Player")  // Verifica se o objeto em que está anexado é o player
        {
            playerAnimation = GetComponent<PlayerAnimation>();
        }
     
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;                 // Armazena o valor da vida máxima na vida atual
        startColor = spriteRenderer.color;         // Armazena a cor padrão do Sprite
    }


    #region Take Damage

    public void TakeDamage(int currentDamage) // Função que recebe o dano
    {
       if (isDead || invincible)    // Retorna ao estar morto ou invulneravel
            return;

        currentHealth -= currentDamage;   

        if(currentHealth <= 0)    // Verifica se está morto
        {
            //Morreu
            isDead = true;
            OnDeath.Invoke();                      // Chama o Evento de Morte
            StopAllCoroutines();                   // Para as corrotinas como a de dano
            spriteRenderer.color = startColor;     // passa a cor padrão caso morra com o sprite na cor do Dano

            return;
        }

        //Essa parte só é executada caso não esteja morto!
        invincible = true;                             // Deixa invencivel
        OnDamage.Invoke();                             // Chama o Evento de Dano
        Invoke("FinishInvincible", invincibleTime);    // Executa a lógica de invencibilidade

    }

    void FinishInvincible() // Função que finaliza a lógica de invencibilidade
    {
        invincible = false;
    }

    #endregion


    #region Invincible

    public void StartInvincibleTransparence() // Função que inicia a corrotina de invencibilidade
    {
        StartCoroutine(ShowInvincibleTransparence());
    }

    IEnumerator ShowInvincibleTransparence()  // faz a Invencibilidade e executa o feedback no Sprite
    {
        if(gameObject.name == "Player") // Caso seja o Player Executa a animação de Hit
        {
            playerAnimation.SetHit();
            yield return new WaitForSeconds(0.42f);  // Espera o fim da animação de Hit para iniciar o efeito de transparência no sprite
            OnExitDamage.Invoke();                   // Chama o Evento que Sai do Dano
        }


        float timer = 0;    // Variável local para o contador

        while(timer < invincibleTime) //Enquanto o timer for menor que o tempo de invencibilidade essa função faz o efeito de piscar do sprite
        {
            spriteRenderer.color = damageColor;      //Passa a cor do dano

            yield return new WaitForSeconds(0.1f);
            timer += 0.1f;                          // Aumenta o contador até que se iguale ao tempo de invencibilidade
            spriteRenderer.color = startColor;      //Passa a cor padrão do sprite

            yield return new WaitForSeconds(0.1f);
            timer += 0.1f;                           // Aumenta o contador até que se iguale ao tempo de invencibilidade
        }

        spriteRenderer.color = startColor;           //Passa a cor padrão do sprite, para que não termine a função na cor de dano
    }
#endregion

}
