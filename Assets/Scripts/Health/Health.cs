using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    //References
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set;} // access modifier
    private Animator anime;

    // Start is called before the first frame update
    void Start()
    {
        anime = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // testing player damage
        // if(Input.GetKeyDown(KeyCode.E))
        //     TakeDamage(1);
    }
    private void Awake()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0 , startingHealth); // ensures you dont go pass zero hp

        if(currentHealth > 0)
        {
            //player =  hurt
            anime.SetTrigger("hurt")
        }
        else 
        {
            //player  = dead
            anime.SetTrigger("dead")
        }
        
    }
}
