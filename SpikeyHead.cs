using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeyHead : EnemyDamage //inheritance
{
    [Header ("Spikey's Attributes")]
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask playerLayer;


    private Vector3 destination;
    private Vector3[] directions = new Vector3[4]; // directions = 4 elements
    private bool attacking;
    private float checkTimer;

    


    private void OnEnable()
    {
        Stop(); //object starts in idle position
    }

    private void Update()
    {
        //if spikey is attacking, mvoe to destination
        if (attacking)
        {
             transform.Translate(destination * Time.deltaTime * speed);
        }
        else
        {
            checkTimer += Time.deltaTime;

            if (checkTimer > checkDelay)
            {
                CheckForPlayer();
            }
        }
           
    }

    private void CalculateDirections()
    {
        directions[0] = transform.right * range; //spikey going right
        directions[1] = -transform.right * range; //spikey going left
        directions[2] = transform.up * range; //spikey going up
        directions[3] = -transform.up * range; //spikey going down
    }
    private void CheckForPlayer()
    {
        CalculateDirections();

        //Will Spikey see char in all given directions
        for (int i = 0; i < directions.Length; i++)
        {
            Debug.DrawRay(transform.position, directions[i], Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);

            if (hit.collider != null && !attacking)
            {
                attacking = true;
                destination = directions[i];
                checkTimer = 0;
            }
        }
    }

    public void Stop()
    {
        destination = transform.position; //direction is set as current position
        attacking = false;
    }

    private void OnTriggerEnter2D(Collider2D col) 
    {
        base.OnTriggerEnter2D(col); //pulling script method from enemydamage

        //stopping spikey
        Stop();
    }
}
