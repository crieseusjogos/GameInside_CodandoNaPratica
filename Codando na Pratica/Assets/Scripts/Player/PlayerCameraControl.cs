using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;



public class PlayerCameraControl : MonoBehaviour
{

    private PlayerInput playerInput;                   // Armazena o Script que gerencia os Inputs do Player

    private Transform cameraTarget;                    // Armazena o GameObject filho do Player, responsável de ser o target para as cameras
    private GameObject cameraConfiner;                 // Armazena o Gameobject responsável por fazer o confinamento das cameras 

    private CinemachineVirtualCamera vCam;             // Armazena o component Virtual Camera do Cinemachine
    private CinemachineConfiner cinemachineConfiner;   // Armazena o component Confiner do Cinemachine




    private void Awake()
    {
        vCam = GameObject.FindObjectOfType<CinemachineVirtualCamera>();             // Referencia o component Virtual Camera do Cinemachine
        cinemachineConfiner = GameObject.FindObjectOfType<CinemachineConfiner>();   // Referencia o component Confiner do Cinemachine

        cameraTarget = transform.GetChild(2);                                       // Referencia o target para as cameras
        cameraConfiner = GameObject.FindGameObjectWithTag("Cinemachine Confiner");  // Referencia o confiner da cena atual (Precisa estar com está tag para ser encontrado)

        playerInput = GetComponent<PlayerInput>();                                  // Referencia o Script que gerencia os Inputs do Player

    }



    private void Start()
    {
        SetCameraReference();   // Função que faz as referencias ao Cinemachine 
    }


    public void SetCameraReference()  // Função chamada a cada novo carregamento de cena, para referenciar os GameObjects ao Cinemachine
    {
        playerInput.enabled = true; // Reabilita a movimentação do Player na nova cena

        LevelController.levelController.SetFadeOff();                               // Executa o efeito de Fade                         
        cameraConfiner = GameObject.FindGameObjectWithTag("Cinemachine Confiner");  // Busca o game object responsável por fazer o Confiner da cena Atual

        vCam.Follow = cameraTarget;   // Referencia o Target do Player, no Cinemachine 
        cinemachineConfiner.m_BoundingShape2D = cameraConfiner.GetComponent<PolygonCollider2D>();  // Referencia o colisor do Confiner no Cinemachine

    }



}
