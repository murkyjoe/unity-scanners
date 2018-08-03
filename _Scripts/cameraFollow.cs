using UnityEngine;
using System.Collections;

public class cameraFollow : MonoBehaviour
{

    public Transform target;
    public float smoothing = 5f;
    bool oneLock;



    Vector3 movement;

    
    void Start()
    {

        movement = transform.position - target.position;
    }

  
    void FixedUpdate()
    {

        bool oneLock = false;

        if (target.position.x < -65 || target.position.x > 65)
        {
            if (oneLock == true)
            {
                transform.position = new Vector3(transform.position.x, target.position.y, -50);
                oneLock = false;
            }
            else
            {
                transform.position = new Vector3(transform.position.x, target.position.y, -50);
                oneLock = true;
            }

        }

        if (target.position.y < -40 || target.position.y > 40)
        {
            if (oneLock == true)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, -50);
                oneLock = false;
            }
            else
            {
                transform.position = new Vector3(target.position.x, transform.position.y, -50);
                oneLock = true;
            }

        }

        else if (oneLock == false)
        {
            Vector3 targetCamPos = target.position + movement;
            transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
        }
    }
}
