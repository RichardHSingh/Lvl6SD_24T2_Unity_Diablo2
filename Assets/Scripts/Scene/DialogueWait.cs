using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueWait : MonoBehaviour
{
    public float waitTime = 20f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DialogueDelay());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DialogueDelay()
    {
        yield return new WaitForSeconds(waitTime);

        // 2 is index --> found in build setting
        SceneManager.LoadScene(3);
    }
}
