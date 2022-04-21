using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveGroudSpikes : MonoBehaviour
{
    [Header("Spikes Settings:")]
    [SerializeField] private GameObject[] leftSpikes; // Conjunto de espinhos que avan�a para a esquerda
    [SerializeField] private GameObject[] rightSpikes; // Conjunto de espinhos que avan�a para a direita
    [SerializeField] private float activeNextSpike;    // Tempo esperado para ativar o pr�ximo espinho



    // Start is called before the first frame update
    void Start()
    {
        // Inicia a ativa��o dos espinhos

        StartCoroutine("LeftSpike", 0.1f);
        StartCoroutine("RightSpike", 0.1f);
        Destroy(gameObject, 5f);             // Destroy os espinhos ap�s todos serem ativados.

    }

    #region Active Spikes

    IEnumerator LeftSpike() // Ativa em sequ�ncia os espinhos da esquerda
    {
        int i = 0;


        while (i < leftSpikes.Length - 1)
        {
            i++;
            leftSpikes[i].SetActive(true);
            yield return new WaitForSeconds(activeNextSpike);
        }

    }


    IEnumerator RightSpike() // Ativa em sequ�ncia os espinhos da direita
    {
        int i = 0;


        while (i < rightSpikes.Length - 1)
        {
            i++;
            rightSpikes[i].SetActive(true);
            yield return new WaitForSeconds(activeNextSpike);
        }

    }


    #endregion

}
