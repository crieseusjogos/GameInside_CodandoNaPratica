using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveGroudSpikes : MonoBehaviour
{

    [SerializeField] private GameObject[] leftSpikes;
    [SerializeField] private GameObject[] rightSpikes;

    [SerializeField] private float activeNextSpike;



    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("LeftSpike", 0.1f);
        StartCoroutine("RightSpike", 0.1f);
        Destroy(gameObject, 5f);

    }

   
    IEnumerator LeftSpike()
    {
        int i = 0;


        while (i < leftSpikes.Length - 1)
        {
            i++;
            leftSpikes[i].SetActive(true);
            yield return new WaitForSeconds(activeNextSpike);
        }

    }


    IEnumerator RightSpike()
    {
        int i = 0;


        while (i < rightSpikes.Length - 1)
        {
            i++;
            rightSpikes[i].SetActive(true);
            yield return new WaitForSeconds(activeNextSpike);
        }

    }




}
