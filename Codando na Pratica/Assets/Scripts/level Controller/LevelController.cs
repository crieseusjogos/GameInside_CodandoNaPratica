using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{

    public static LevelController levelController;   // Deixa está classe acessivel de outros scripts 

    [SerializeField] private int _doorNumber;        // Armazena o valor do id da Porta, para ser comparado na próxima cena
    [SerializeField] private int _numberLevel;       // Armazena o index da próxima cena que deverá ser carregada

    [SerializeField] private Animator fadeAnim;      // Armazena o Animator da imagem que faz o Fade entre cenas / É referenciado manualmente no inspector


    #region Encapsulamento
    public int doorNumber { get => _doorNumber; set => _doorNumber = value; }
    public int numberLevel { get => _numberLevel; set => _numberLevel = value; }

    #endregion


    private void Awake()
    {
        // Verificação que impede a criação de duplicatas no jogo

        if(levelController == null)          // Verifica se a variável está nula
        {
            levelController = this;          // Armazena este Game Object na variável
            DontDestroyOnLoad(this);         // Função que impede o game Object de ser destruido ao transitar entre cenas
        }
        else if(levelController != this)    // Verifica se o Game Object aramazenado é igual ao primeiro Game Object armazenado na variável
        {
            Destroy(gameObject);            // Caso seja diferente ele é destruido
        }
    }


    private void Start()
    {
        SetFadeOff();                    // Executa o Efeito de Fade
    }


    #region  Loading levels

    public void NewLevel(int numberDoor, int intLevel)    //Função responsável por receber e armazenar os dados recebidos das Doors
    {
        doorNumber = numberDoor;                          // Armazena o Id da porta colidida
        numberLevel = intLevel;                           // Armazena o index da próxima cena
        SetFadeOn();                                      // Executa o efeito de Fade
         
        Invoke("GoNewLevel", 1.3f);                       // Chama a função que faz a carregamento da cena, esperando o tempo da animação de Fade

    }

    public void GoNewLevel()                            // Função que carrega as próximas cenas
    {
        fadeAnim.Play("Fade Idle", -1);                 // Executa a animação do fade totalmente escuro
        SceneManager.LoadScene(numberLevel);            // Carrega a cena com o idex recebido
    }

    #endregion

    #region Fade 

    public void SetFadeOn()  // Executa o fade que vai Escurecendo a tela
    {
        fadeAnim.Play("Fade Enabled", -1);
    }

    public void SetFadeOff() // Executa o fade que vai Clareando a cena
    {
        fadeAnim.Play("Fade Disabled", -1);
    }

    #endregion
}
