using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Wait : MonoBehaviour
{
    public float waitTime = 12f;
    public string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(IntroDelay());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator IntroDelay()
    {
        yield return new WaitForSeconds(waitTime);

        // 2 is index --> found in build setting
        SceneManager.LoadScene(sceneName);
    }
}
