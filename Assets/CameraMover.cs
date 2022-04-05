using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    Vector3 oldPosition;

    void Start()
    {
        oldPosition = transform.position;
    }

    void Update()
    {
        CheckIfCameraMoved();
    }

    void CheckIfCameraMoved()
    {
        if (oldPosition != transform.position)
        {
            oldPosition = transform.position;

            
        }
    }
}
