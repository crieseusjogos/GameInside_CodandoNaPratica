using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField] private int _damage;


    #region Encapsulamento
    public int damage { get => _damage; set => _damage = value; }

    #endregion


    private void OnTriggerStay2D(Collider2D other) // Acessa o Script de Controle de Dano em outro objeto e passa o dano causado 
    {
        DamageControl damageControl = other.GetComponent<DamageControl>();
        
        if(damageControl != null)
        {
            damageControl.TakeDamage(damage);
        }

    }



}
