using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]  private float attackCooldown;
    [SerializeField]  private Transform firePoint;
    [SerializeField]  private GameObject[] fireballs;

    [Header ("Sound Effects")]
    [SerializeField]  private AudioClip swordAttack;
    


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
        if(Input.GetKeyDown(KeyCode.Z) && cooldownTimer > attackCooldown && playerMovement.canAttack())
        {
            if (!hasAttacked)
            {
                Attack();
                hasAttacked = true; // Set the flag to true to indicate that an attack has occurred
            } 
        }
        
        cooldownTimer += Time.deltaTime;

        if (Input.GetKeyUp(KeyCode.Z))
        {
            hasAttacked = false;
        }


        if(Input.GetKeyDown(KeyCode.X) && cooldownTimer > attackCooldown && playerMovement.canAttack())
        {
            if (!hasAttacked)
            {
                FireBall();
                hasAttacked = true; // Set the flag to true to indicate that an attack has occurred
            } 
        }
        
        cooldownTimer += Time.deltaTime;

        if (Input.GetKeyUp(KeyCode.X))
        {
            hasAttacked = false;
        }
    }

    private void Attack()
    {
        SoundManager.instance.PlaySound(swordAttack);
        anime.SetTrigger("attack");
        cooldownTimer = 0;
    }
    private void FireBall()
    {
        SoundManager.instance.PlaySound(swordAttack);
        anime.SetTrigger("fireball");
        cooldownTimer = 0;

        //object pooling for fireball
        fireballs[FindFireBall()].transform.position = firePoint.position;
        fireballs[FindFireBall()].GetComponent<FireBallJutsu>().SetDirection(Mathf.Sign(transform.localScale.x)); 
    }

    private void OnTriggerEnter2D(Collider2D col) 
    {
        if(col.tag == "Enemy")
        {
            col.GetComponent<Health>().TakeDamage(1);
        }
    }

    private int FindFireBall()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if(fireballs[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }
    
}
