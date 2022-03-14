using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PlayerMoviment playerMoviment;



    private void Awake()
    {
        playerMoviment = GetComponent<PlayerMoviment>();
    } 

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        GetMovimentInput();
    }
 



    void GetMovimentInput()
    {
        playerMoviment.OnMove(Input.GetAxisRaw("Horizontal"));
    }




}
