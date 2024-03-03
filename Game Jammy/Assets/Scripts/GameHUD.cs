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
    public AudioClip menuMusic;
    public AudioClip playMusic;
    public AudioSource playMusicSource;
    private TextMeshProUGUI _message;
    private Image _menuOverlay;
    private GameObject _button;
    private TextMeshProUGUI _score;
    private GameObject _instruction;
    private RawImage[] _actionImages = new RawImage[3];
    private Attack[] _action = new Attack[2];
    public int civiliansAlive;
    public float timeRemaining;

    void Awake()
    {
        state = GameState.MainMenu;
        _message = GameObject.Find("Message").GetComponent<TextMeshProUGUI>();
        _menuOverlay = GameObject.Find("MenuOverlay").GetComponent<Image>();
        _button = GameObject.Find("Button");
        _score = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
        _instruction = GameObject.Find("Instruction");
        _actionImages = _instruction.GetComponentsInChildren<RawImage>();
        GameObject player = GameObject.FindWithTag("Player");
        _action[0] = player.GetComponentInChildren<Punch>();
        _action[1] = player.GetComponentInChildren<Stomp>();
    }

    
    void Update()
    {
        switch(state)
        {
            case GameState.MainMenu:
                _message.text = "<size=60>Is it Big WEEVIL?</size>\n<size=20>or smol ppl?</size>";
                _menuOverlay.enabled = true;
                _score.enabled = false;
                _instruction.SetActive(false);
                break;
            case GameState.Initialize:
                _message.enabled = false;
                _menuOverlay.enabled = false;
                _button.SetActive(false);
                _score.enabled = true;
                Camera.main.GetComponent<CameraController>().cameraState = CameraController.CameraStates.Following;
                _instruction.SetActive(true);
                playMusicSource.clip = playMusic;
                playMusicSource.Play();
                state = GameState.Playing;
                break;
            case GameState.Playing:
                _score.text = $"Victims To KILL: {civiliansAlive}\nTime Remaining: {timeRemaining:0.00}";
                timeRemaining -= Time.deltaTime;

                for (int i = 0; i < _action.Length; i++)
                {
                    Color tempColour = _actionImages[i+1].color;
                    tempColour.a = Mathf.Abs(_action[i].currentAttackCooldown / _action[i].attackCooldown - 1);
                    _actionImages[i+1].color = tempColour;
                }

                if (timeRemaining <= 0)
                {
                    timeRemaining = 0;
                    state = GameState.InitializeGameOver; 
                }
                break;
            case GameState.InitializeGameOver:
                playMusicSource.clip = menuMusic;
                playMusicSource.Play();
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
