using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    #region Fields
    private GameObject _player;
    private GameHUD _hud;
    #endregion

    #region Properties
    public static WorldManager Global { get; private set; }
    public float Gravity { get; private set; } = -9.81f;
    public Vector3 PlayerPos { get; private set; }
    public bool isPlaying 
    { 
        get { return _hud.state == GameHUD.GameState.Playing; }
    }
    public float timeRemaining
    {
        get { return _hud.timeRemaining; }
    }
    #endregion

    private void Awake()
    {
        if ( Global is not null && Global != this)
        { Destroy(gameObject); }
        else
        { Global = this; }
        Gravity = Global.Gravity;
        _player = GameObject.FindGameObjectWithTag("Player");
        _hud = GameObject.FindGameObjectWithTag("HUD").GetComponent<GameHUD>();
    }
    private void Update()
    {
        PlayerPos = _player.transform.position;
    }

}
