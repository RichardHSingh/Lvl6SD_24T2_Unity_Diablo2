using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
        //references
    [SerializeField] private Vector3 offset = new Vector3(0f, 5f, -10f);
    [SerializeField] private Transform target;
    [SerializeField] private GameObject background;
    
    private float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;
    private Vector3 backgroundOffset;
   


    // Start is called before the first frame update
    void Start()
    {
        if(background)
        {
            backgroundOffset = background.transform.position - transform.position;
        }
        else
        {
            Debug.LogWarning("Background is not assigned");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        // having background follow char/camera
        if(background)
        {
            background.transform.position = transform.position + backgroundOffset;
        }
    }
}
