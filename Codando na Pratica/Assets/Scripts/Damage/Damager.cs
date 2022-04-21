using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField] private int _damage;   // Valor do Dano


    #region Encapsulamento
    public int damage { get => _damage; set => _damage = value; }

    #endregion


    private void OnTriggerStay2D(Collider2D other) // Acessa o Script de Controle de Dano em outro objeto e passa o dano causado 
    {
        DamageControl damageControl = other.GetComponent<DamageControl>(); // Armazena localmente o script que controla o dano
        
        if(damageControl != null)   // Verifica se o objeto da colisão possue um Controlador de Dano
        {
            damageControl.TakeDamage(damage);   // Acessa a Função que recebe o dano no controlador, e passa o valor do dano causado
        }

    }



}
