using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header ("Health")]
    [field: SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anime;
    private bool dead;


    [Header ("iFrame")]
    [field: SerializeField] private float iFramesDuration;
    [field: SerializeField] private float numberOfFlashes;
    private SpriteRenderer spriteRend;


    [Header ("Sound Effects")]
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private AudioClip hpSound;


    [Header ("Components")]
    [SerializeField] private Behaviour[] components;
    private bool invulnerable;

    private void Awake()
    {
        currentHealth = startingHealth;
        anime = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float _damage)
    {
        
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth); // ensures you don't go past zero hp

        if (currentHealth > 0)
        {
            // player = hurt
            anime.SetTrigger("hurt");
            SoundManager.instance.PlaySound(hurtSound);
            
            // iframes
            StartCoroutine(Invulnerability());
        }
        else
        {
            // player = dead
            if (!dead)
            {
                Die();

                // deactivate all attached components
                foreach (Behaviour component in components)
                {
                    component.enabled = false;
                }

                anime.SetBool("grounded", false);
                anime.SetTrigger("dead");

                dead = true;
                SoundManager.instance.PlaySound(deathSound);
            }
        }
    }

    public void AddHealth(float _value)
    {
      
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
        SoundManager.instance.PlaySound(hpSound);
    }

    private IEnumerator Invulnerability()
    {
        // 10 and 11 refer to the layer created for player and enemy and collisions will be ignored
        Physics2D.IgnoreLayerCollision(10, 11, true);

        // time to wait to be hit again
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f); // changes char to red and .5 is transparency
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);
    }

    private void Die()
    {
        anime.SetTrigger("dead");

        // character
        if (GetComponent<PlayerMovement>() != null)
        {
            GetComponent<PlayerMovement>().enabled = false;
        }

        // enemy
        if (GetComponentInParent<EnemyPatrol>() != null)
        {
            GetComponentInParent<EnemyPatrol>().enabled = false;
        }

        if (GetComponent<MeleeEnemy>() != null)
        {
            GetComponent<MeleeEnemy>().enabled = false;
        }

        dead = true;
    }

    public void Respawn()
    {
        dead = false;
        currentHealth = startingHealth;

        // what scenes to play at respawn
        anime.ResetTrigger("dead");
        anime.Play("Idle");

        // makes player not targetable when coming back to life
        StartCoroutine(Invulnerability());

        // activate all attached components
        foreach (Behaviour component in components)
        {
            component.enabled = true;
        }

        // re-enable player movement
        if (GetComponent<PlayerMovement>() != null)
        {
            GetComponent<PlayerMovement>().enabled = true;
        }
    }
}
