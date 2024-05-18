using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]  private float attackCooldown;
    private Animator anime;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;
    private bool hasAttacked = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        // References to grab 
        // [ first = animator | second = playermovement ]
        anime = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
       
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovement.canAttack())
           if (!hasAttacked)
            {
                Attack();
                hasAttacked = true; // Set the flag to true to indicate that an attack has occurred
            } 
        
        cooldownTimer += Time.deltaTime;

        if (Input.GetMouseButtonUp(0))
        {
            hasAttacked = false;
        }
    }

    private void Attack()
    {
        anime.SetTrigger("attack");
        cooldownTimer = 0;
    }
}
