using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{

    public static LevelController levelController;

    [SerializeField] private int _doorNumber;
    [SerializeField] private int _numberLevel;

    public int doorNumber { get => _doorNumber; set => _doorNumber = value; }
    public int numberLevel { get => _numberLevel; set => _numberLevel = value; }

    private void Awake()
    {
        if(levelController == null)
        {
            levelController = this;
            DontDestroyOnLoad(this);
        }
        else if(levelController != this)
        {
            Destroy(gameObject);
        }
    }






    public void NewLevel(int numberDoor, int intLevel)
    {
        doorNumber = numberDoor;
        numberLevel = intLevel;

        Invoke("GoNewLevel", 1.3f);

    }

    public void GoNewLevel()
    {
        SceneManager.LoadScene(numberLevel);
    }


}
