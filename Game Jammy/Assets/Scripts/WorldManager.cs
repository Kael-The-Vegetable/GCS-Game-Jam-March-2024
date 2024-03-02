using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public static WorldManager Global { get; private set; }
    public float Gravity { get; private set; } = 9.81f;
    private void Awake()
    {
        if ( Global is not null && Global != this)
        { Destroy(gameObject); }
        else
        { Global = this; }
        Gravity = Global.Gravity;
    }

    
}
