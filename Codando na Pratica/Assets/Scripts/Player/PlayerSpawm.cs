using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawm : MonoBehaviour
{

    public static PlayerSpawm playerSpawm;              // Deixa essa classe acessível de outros scripts

    private PlayerCameraControl playerCameraControl;    // Armazena o Script que gerencia as referencias das Cameras do Cinemachine

    private void Awake()
    {
        // Verificação que impede a duplicação do Player na troca de cenas

        if(playerSpawm == null)                    // Verifica se a variável é nula
        {
            playerSpawm = this;                    // Caso seja nula, referencia essa classe nela
            DontDestroyOnLoad(this);               // Impede de ser destruido nas transições entre cenas
        }
        else if(playerSpawm != this)               // Verifica se a primeira classe armazenada na variável é igual a essa
        {
            Destroy(gameObject);                   // Caso seja diferente, destroy ela
        }


        playerCameraControl = GetComponent<PlayerCameraControl>();  // Referencia o Script que gerencia as referencias das Cameras do Cinemachine


    }

 
    public void SetNewCamera() // Função que acessa o script referencia as cameras
    {
        playerCameraControl.SetCameraReference();
    }



}
