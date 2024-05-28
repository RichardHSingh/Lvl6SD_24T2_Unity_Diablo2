using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpointSound; // play will sound when engaged with checkpoint
    [SerializeField] private AudioClip respawnSound; // play will sound when engaged with checkpoint


    private Transform currentCheckpoint; // we'll store latest checkpoint
    private Health playerHealth;


    private void Awake() 
    {
        playerHealth = GetComponent<Health>();
    }

    private void Update()
    {

    }


    public void Respawn()
    {
        SoundManager.instance.PlaySound(respawnSound);
        transform.position = currentCheckpoint.position; //moves player to checkpoint position

        //Restoring player HP and reset animation
        playerHealth.Respawn();

        //reset camera to player
        //Camera.main.GetComponent<CameraFollow>();
    }

    //Activate Checkpoint
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag == "Checkpoint")
        {
            currentCheckpoint = col.transform; // store checkpoint activated as current one
            SoundManager.instance.PlaySound(checkpointSound);
            col.GetComponent<Collider2D>().enabled = false;   //deactivates the checkpoint collider
            col.GetComponent<Animator>().SetTrigger("appear"); // triggerscheckpoint animation
        }
    }
}
