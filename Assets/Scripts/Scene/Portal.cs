using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public SceneController sc;
    public string sceneName;

    public void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            sc.SceneChange(sceneName);
        }
    }
    
}
