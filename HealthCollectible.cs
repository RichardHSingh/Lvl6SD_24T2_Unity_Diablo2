using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{

    [SerializeField] private float healthValue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   private void OnTriggerEnter2D(Collider2D hp) 
   {
        if (hp.tag == "Player")
        {
            hp.GetComponent<Health>().AddHealth(healthValue);
            gameObject.SetActive(false); //deactives the heart so it cant be picked up over n over again
        }
   }
}
