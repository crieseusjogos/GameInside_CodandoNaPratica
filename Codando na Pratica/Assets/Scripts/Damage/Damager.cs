using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField] private int _damage;

    public int damage { get => _damage; set => _damage = value; }



    private void OnTriggerStay2D(Collider2D other)
    {
        DamageControl damageControl = other.GetComponent<DamageControl>();
        
        if(damageControl != null)
        {
            damageControl.TakeDamage(damage);
        }

    }




}
