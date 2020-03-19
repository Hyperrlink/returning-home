using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public bool triggered = false;
    public bool upStopped = false;
    public bool downStopped = true;

    void Start()
    {
        
    }

    void Update()
    {

        if (triggered && !upStopped && downStopped)
        {

            transform.position = new Vector3(transform.position.x, transform.position.y + 5, transform.position.z);
            upStopped = true;
            downStopped = false;

        }
        else if (!triggered && upStopped && !downStopped) {

            transform.position = new Vector3(transform.position.x, transform.position.y - 5, transform.position.z);
            upStopped = false;
            downStopped = true;

        }

    }
}
