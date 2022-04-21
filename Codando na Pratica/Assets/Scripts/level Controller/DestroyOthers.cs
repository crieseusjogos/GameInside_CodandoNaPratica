using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOthers : MonoBehaviour
{
    public static DestroyOthers destroyOthers; // Deixa a classe acessivel em outros scripts


    private void Awake()
    {

        //Essa verifica��o impede a duplica��o de objetos na cena, o que pode trazer diversos bugs!

        if(destroyOthers == null)     // Verifica se a vari�vel est� vazia para armazenar este Game Object nela
        {
            destroyOthers = this;
        }
        else if(destroyOthers != this) // Verifica se o Game Object armazenado nessa vari�vel � o primeiro que foi armazenado, caso n�o seja, ele � destruido!
        {
            Destroy(gameObject);
        }
    }


}
