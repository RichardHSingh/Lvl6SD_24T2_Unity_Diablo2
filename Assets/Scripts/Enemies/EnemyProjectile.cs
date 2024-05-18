using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : EnemyDamage //damage player everytime it touches - inherited from EnemyDamage (before was MonoBehaviour)
{
    // though there is dmg serializefield - its coming from enemydmage that the script is inheriting from
    // as seen, there is no dmg variable here

    [SerializeField] private float speed;
    [SerializeField] private float resetTime;

    private float lifetime;
    public void ActivateProjectile()
    {
        lifetime = 0;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(movementSpeed,0 , 0);

        lifetime += Time.deltaTime;

        if (lifetime > resetTime)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D col) 
    {
        base.OnTriggerEnter2D(col); // allows the access to the parent ontrigger script
        gameObject.SetActive(false); //deactives object after hitting a collider
    }
}
