using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawm : MonoBehaviour
{

    public static PlayerSpawm playerSpawm;



    private void Awake()
    {
        if(playerSpawm == null)
        {
            playerSpawm = this;
            DontDestroyOnLoad(this);
        }
        else if(playerSpawm != this)
        {
            Destroy(gameObject);
        }
    }



}
