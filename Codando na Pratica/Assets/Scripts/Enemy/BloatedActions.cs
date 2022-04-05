using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloatedActions : MonoBehaviour
{
    [SerializeField] private Transform rayPoint;
    [SerializeField] private float rayDistance;







    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        DetectPlayer();
    }



    void DetectPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(rayPoint.position, Vector2.left * transform.localScale.x, rayDistance);

        if(hit.collider != null)
        {
            if(hit.transform.CompareTag("Player"))
            {
                float playerDistance = transform.position.x - hit.point.x;


                if(Mathf.Abs(playerDistance) <= 1f)
                {
                    //Ataque Corpo a corpo
                    Debug.Log("ataque corpo a corpo");
                }
                else if(Mathf.Abs(playerDistance) > 1f)
                {
                    //Ataque a distância
                    Debug.Log("ataque a distância");
                }


            }
        }


    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(rayPoint.position, Vector2.left * transform.localScale.x * rayDistance);
    }

}
