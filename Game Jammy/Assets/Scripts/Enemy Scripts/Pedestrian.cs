using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedestrian : Actor
{
    public Vector3 targetDestination;

    void Update()
    {
        Move(targetDestination);
    }
}
