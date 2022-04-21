using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawm : MonoBehaviour
{

    public static PlayerSpawm playerSpawm;              // Deixa essa classe acess�vel de outros scripts

    private PlayerCameraControl playerCameraControl;    // Armazena o Script que gerencia as referencias das Cameras do Cinemachine

    private void Awake()
    {
        // Verifica��o que impede a duplica��o do Player na troca de cenas

        if(playerSpawm == null)                    // Verifica se a vari�vel � nula
        {
            playerSpawm = this;                    // Caso seja nula, referencia essa classe nela
            DontDestroyOnLoad(this);               // Impede de ser destruido nas transi��es entre cenas
        }
        else if(playerSpawm != this)               // Verifica se a primeira classe armazenada na vari�vel � igual a essa
        {
            Destroy(gameObject);                   // Caso seja diferente, destroy ela
        }


        playerCameraControl = GetComponent<PlayerCameraControl>();  // Referencia o Script que gerencia as referencias das Cameras do Cinemachine


    }

 
    public void SetNewCamera() // Fun��o que acessa o script referencia as cameras
    {
        playerCameraControl.SetCameraReference();
    }



}
