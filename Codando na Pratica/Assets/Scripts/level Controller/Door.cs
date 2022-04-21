using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    [SerializeField] private int _doorId;    // Armazena o número de identificação desta porta
    [SerializeField] private int _levelId;   // Armazena o valor inteiro do index da cena, que queira carregar, lá do Build Settings

    #region Encapsulamento

    public int doorId { get => _doorId; set => _doorId = value; }
    public int levelId { get => _levelId; set => _levelId = value; }

    #endregion

    private void Start()
    {
        if(LevelController.levelController.doorNumber == doorId) // Acessa o level Controler para verificar qual é o idex de porta armazenado
        {
            PlayerSpawm.playerSpawm.transform.position = transform.position;  // Caso seja compativel com a porta de mesmo Id, ela passa sua posição para o player
            PlayerSpawm.playerSpawm.SetNewCamera();                           // Aciona a função responsável por passar as novas referências, para a camera do Cinemachine.
        }
    }



    private void OnTriggerEnter2D(Collider2D other) // Detecta a colisão com o Player e inicia a lógica para ir para uma nova cena 
    {
        
        if(other.CompareTag("Player"))  // Caso seja o Player, vai para próxima Cena.
        {
            other.GetComponent<PlayerInput>().enabled = false;           // Desabilita o Script de Inputs, para impedir a movimentação do Player
            other.GetComponent<Rigidbody2D>().velocity = Vector2.zero;   // Tira a velocidade existente no Player

            LevelController.levelController.NewLevel(doorId, levelId);   // Acessa a função que carrega a cena,e passa o id da porta colidida e o index da próxima cena
        }

    }


}
