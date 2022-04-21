using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{

    public static LevelController levelController;   // Deixa est� classe acessivel de outros scripts 

    [SerializeField] private int _doorNumber;        // Armazena o valor do id da Porta, para ser comparado na pr�xima cena
    [SerializeField] private int _numberLevel;       // Armazena o index da pr�xima cena que dever� ser carregada

    [SerializeField] private Animator fadeAnim;      // Armazena o Animator da imagem que faz o Fade entre cenas / � referenciado manualmente no inspector


    #region Encapsulamento
    public int doorNumber { get => _doorNumber; set => _doorNumber = value; }
    public int numberLevel { get => _numberLevel; set => _numberLevel = value; }

    #endregion


    private void Awake()
    {
        // Verifica��o que impede a cria��o de duplicatas no jogo

        if(levelController == null)          // Verifica se a vari�vel est� nula
        {
            levelController = this;          // Armazena este Game Object na vari�vel
            DontDestroyOnLoad(this);         // Fun��o que impede o game Object de ser destruido ao transitar entre cenas
        }
        else if(levelController != this)    // Verifica se o Game Object aramazenado � igual ao primeiro Game Object armazenado na vari�vel
        {
            Destroy(gameObject);            // Caso seja diferente ele � destruido
        }
    }


    private void Start()
    {
        SetFadeOff();                    // Executa o Efeito de Fade
    }


    #region  Loading levels

    public void NewLevel(int numberDoor, int intLevel)    //Fun��o respons�vel por receber e armazenar os dados recebidos das Doors
    {
        doorNumber = numberDoor;                          // Armazena o Id da porta colidida
        numberLevel = intLevel;                           // Armazena o index da pr�xima cena
        SetFadeOn();                                      // Executa o efeito de Fade
         
        Invoke("GoNewLevel", 1.3f);                       // Chama a fun��o que faz a carregamento da cena, esperando o tempo da anima��o de Fade

    }

    public void GoNewLevel()                            // Fun��o que carrega as pr�ximas cenas
    {
        fadeAnim.Play("Fade Idle", -1);                 // Executa a anima��o do fade totalmente escuro
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
