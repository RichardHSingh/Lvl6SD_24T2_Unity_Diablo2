using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] protected float damage;


    //sole purpose is to dmg the character
    protected void OnTriggerEnter2D(Collider2D col) //protected allows access from other scripts
    {
        if (col.tag == "Player")
            col.GetComponent<Health>().TakeDamage(damage);
    }
}
