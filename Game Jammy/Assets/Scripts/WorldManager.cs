using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public static WorldManager Global { get; private set; }
    public float Gravity { get; private set; } = -9.81f;
    public Vector3 PlayerPos { get; private set; }
    private GameObject _player;
    private void Awake()
    {
        if ( Global is not null && Global != this)
        { Destroy(gameObject); }
        else
        { Global = this; }
        Gravity = Global.Gravity;
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        PlayerPos = _player.transform.position;
    }

}
