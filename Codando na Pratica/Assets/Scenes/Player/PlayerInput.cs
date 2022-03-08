using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PlayerMoviment playerMoviment;

    private float horizontal = 0f;

    // Start is called before the first frame update
    void Start()
    {
        playerMoviment = GetComponent<PlayerMoviment>();
    }

    // Update is called once per frame
    void Update()
    {
        GetMovimentInput();
   
    }
    private void FixedUpdate()
    {
      
    }

    void GetMovimentInput()
    {
        playerMoviment.OnMove(Input.GetAxisRaw("Horizontal"));
    }




}
