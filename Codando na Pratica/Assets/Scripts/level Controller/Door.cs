using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    [SerializeField] private int _doorId;
    [SerializeField] private int _levelId;

    public int doorId { get => _doorId; set => _doorId = value; }
    public int levelId { get => _levelId; set => _levelId = value; }


    private void Start()
    {
        if(LevelController.levelController.doorNumber == doorId)
        {
            PlayerSpawm.playerSpawm.transform.position = transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if(other.CompareTag("Player"))
        {
            LevelController.levelController.NewLevel(doorId, levelId);
        }

    }


}
