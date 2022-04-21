using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOthers : MonoBehaviour
{
    public static DestroyOthers destroyOthers; // Deixa a classe acessivel em outros scripts


    private void Awake()
    {

        //Essa verificação impede a duplicação de objetos na cena, o que pode trazer diversos bugs!

        if(destroyOthers == null)     // Verifica se a variável está vazia para armazenar este Game Object nela
        {
            destroyOthers = this;
        }
        else if(destroyOthers != this) // Verifica se o Game Object armazenado nessa variável é o primeiro que foi armazenado, caso não seja, ele é destruido!
        {
            Destroy(gameObject);
        }
    }


}
