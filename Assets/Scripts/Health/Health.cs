using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    //References
    [Header ("Health")]
    [field: SerializeField] private float startingHealth;
    public float currentHealth { get; private set;} // access modifier
    private Animator anime;
    private bool dead;

    [Header ("iFrame")]
    [field: SerializeField] private float iFramesDuration;
    [field: SerializeField] private float numberOfFlashes;
    private SpriteRenderer spriteRend;

    private void Awake()
    {
        currentHealth = startingHealth;
        anime = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }


    // Update is called once per frame
    void Update()
    {
        // testing player damage
        // if(Input.GetKeyDown(KeyCode.E))
        //     TakeDamage(1);
    }
 
    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0 , startingHealth); // ensures you dont go pass zero hp

        if(currentHealth > 0)
        {
            //player =  hurt
            anime.SetTrigger("hurt");
            
            //iframes
            StartCoroutine(Invunerability());
        }
        else 
        {
            //player  = dead
            if (!dead)
            {
                anime.SetTrigger("dead");
                GetComponent<PlayerMovement>().enabled = false;
                dead = true;
            }
            
        }
        
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0 , startingHealth);
    }

    //IEnumerator needs StartCoroutine to start the method
    private IEnumerator Invunerability()
    {
        //10 and 11 refer to the layer created for player and enemy and collisions will be ignored
        Physics2D.IgnoreLayerCollision(10,11, true);

        //time to wait to be hit again
        for (int i = 0; i < numberOfFlashes; i++)    
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f); //changes char to red and .5 is transparency
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(10,11, false);
    }
}
