using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameHUD : MonoBehaviour
{
    public enum GameState
    {
        MainMenu,
        Initialize,
        Playing,
        InitializeGameOver,
        GameOver
    }
    public GameState state { get; private set; }
    private TextMeshProUGUI _message;
    private Image _menuOverlay;
    private GameObject _button;
    private TextMeshProUGUI _score;
    public int civiliansAlive;
    public float timeRemaining;

    void Awake()
    {
        state = GameState.MainMenu;
        _message = GameObject.Find("Message").GetComponent<TextMeshProUGUI>();
        _menuOverlay = GameObject.Find("MenuOverlay").GetComponent<Image>();
        _button = GameObject.Find("Button");
        _score = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
    }

    
    void Update()
    {
        switch(state)
        {
            case GameState.MainMenu:
                _message.text = "<size=60>Is it Big WEEVIL?</size>\n<size=20>or smol ppl?</size>";
                _menuOverlay.enabled = true;
                _score.enabled = false;
                break;
            case GameState.Initialize:
                _message.enabled = false;
                _menuOverlay.enabled = false;
                _button.SetActive(false);
                _score.enabled = true;
                Camera.main.GetComponent<CameraController>().cameraState = CameraController.CameraStates.Following;
                state = GameState.Playing;
                break;
            case GameState.Playing:
                _score.text = $"Victims To KILL: {civiliansAlive}\nTime Remaining: {timeRemaining:0.00}";
                timeRemaining -= Time.deltaTime;
                if (timeRemaining <= 0)
                {
                    timeRemaining = 0;
                    state = GameState.InitializeGameOver; 
                }
                break;
            case GameState.InitializeGameOver:

                break;
            case GameState.GameOver:

                break;
        }
    }
    public void Play()
    {
        state = GameState.Initialize;
    }
}
