using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private AudioClip respawnSound;    
    private Transform currentCheckpoint; // we'll store latest checkpoint
    private Health playerHealth;
    private UIManager uiManager;


    private void Awake() 
    {
        playerHealth = GetComponent<Health>();
        uiManager = FindObjectOfType<UIManager>(); //reurn first active loaded object of Type

        if (uiManager == null)
        {
            DontDestroyOnLoad(uiManager.gameObject);
        }
        else
        {
            Debug.LogWarning("UIManager not found.");
        }
    }

    private void Update()
    {

    }


    public void CheckRespawn()
    {
        //check if checkpoint is available
        if (currentCheckpoint == null)
        {
            //shows game over screen
            uiManager.GameOver();

            return; // code below will be ignored 
        }
        
        transform.position = currentCheckpoint.position; //moves player to checkpoint position

        //Restoring player HP and reset animation
        playerHealth.Respawn();
        SoundManager.instance.PlaySound(respawnSound);
        //reset camera to player
        //Camera.main.GetComponent<CameraFollow>();
    }

    //Activate Checkpoint
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag == "Checkpoint")
        {
            currentCheckpoint = col.transform; // store checkpoint activated as current one
            col.GetComponent<Collider2D>().enabled = false;   //deactivates the checkpoint collider
            col.GetComponent<Animator>().SetTrigger("appear"); // triggerscheckpoint animation
        }
    }
}
