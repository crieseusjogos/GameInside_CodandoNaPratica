using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoviment : MonoBehaviour
{
    private Rigidbody2D rig;
    private Vector2 newMoviment;

    [Header("Moviment:")]
    [SerializeField] private float speed;


    public int MyProperty { get; set; }

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Movement();
    }

    public void OnMove(float direction)
    {
        float currentSpeed = speed;

        newMoviment = new Vector2(direction * currentSpeed, rig.velocity.y);

    }

    void Movement()
    {
        rig.velocity = newMoviment;
    }

   }
