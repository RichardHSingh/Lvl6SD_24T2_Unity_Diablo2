using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abyss : MonoBehaviour
{
    public int damage;

    public void OnCollisionStay2D(Collision2D abyss)
    {
        if (abyss.gameObject.CompareTag("Player"))
        {
            Health playerObject = abyss.gameObject.GetComponent<Health>();

            playerObject.TakeDamage(damage);
        }
    }
}
