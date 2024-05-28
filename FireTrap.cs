using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    [SerializeField] private float damage;

    [Header("Firetrap Timers")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;

    private Animator anime;
    private SpriteRenderer spriteRend;

    private bool triggered; // when trap is triggered
    private bool active; // when trap has been activated - damages player

    private void Awake()
    {
        anime = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D col) 
    {
        if (col.gameObject.tag == "Player")
        {
            if (!triggered)
            {
                // triggers trap
                StartCoroutine(ActivateFiretrap());
            }
            if (active)
            {
                col.gameObject.GetComponent<Health>().TakeDamage(damage);
            }
        }
    }

    private IEnumerator ActivateFiretrap()
    {
        // turns sprite to red when close before triggered
        triggered = true;
        spriteRend.color = Color.red; 

        // wait for delay, activate trap, turn on animation, return colour to normal
        yield return new WaitForSeconds(activationDelay);
        // turns sprite backto normal
        spriteRend.color = Color.white; 

        active = true;
        anime.SetBool("activated", true);

        //after a delay, trap deacivated reset bool and animation
        yield return new WaitForSeconds(activeTime);
        active = false;
        triggered = false;
        anime.SetBool("activated", false);
    }
}
