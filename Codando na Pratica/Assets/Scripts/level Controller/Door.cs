using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    [SerializeField] private int _doorId;    // Armazena o n�mero de identifica��o desta porta
    [SerializeField] private int _levelId;   // Armazena o valor inteiro do index da cena, que queira carregar, l� do Build Settings

    #region Encapsulamento

    public int doorId { get => _doorId; set => _doorId = value; }
    public int levelId { get => _levelId; set => _levelId = value; }

    #endregion

    private void Start()
    {
        if(LevelController.levelController.doorNumber == doorId) // Acessa o level Controler para verificar qual � o idex de porta armazenado
        {
            PlayerSpawm.playerSpawm.transform.position = transform.position;  // Caso seja compativel com a porta de mesmo Id, ela passa sua posi��o para o player
            PlayerSpawm.playerSpawm.SetNewCamera();                           // Aciona a fun��o respons�vel por passar as novas refer�ncias, para a camera do Cinemachine.
        }
    }



    private void OnTriggerEnter2D(Collider2D other) // Detecta a colis�o com o Player e inicia a l�gica para ir para uma nova cena 
    {
        
        if(other.CompareTag("Player"))  // Caso seja o Player, vai para pr�xima Cena.
        {
            other.GetComponent<PlayerInput>().enabled = false;           // Desabilita o Script de Inputs, para impedir a movimenta��o do Player
            other.GetComponent<Rigidbody2D>().velocity = Vector2.zero;   // Tira a velocidade existente no Player

            LevelController.levelController.NewLevel(doorId, levelId);   // Acessa a fun��o que carrega a cena,e passa o id da porta colidida e o index da pr�xima cena
        }

    }


}
