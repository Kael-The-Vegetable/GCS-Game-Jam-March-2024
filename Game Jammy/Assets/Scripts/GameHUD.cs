using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
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
    public TextMeshProUGUI buttonText;
    public InputAction quitInput;

    private TextMeshProUGUI _message;
    private Image _menuOverlay;
    private GameObject _button;
    private TextMeshProUGUI _score;
    private GameObject _instruction;
    private RawImage[] _actionImages = new RawImage[3];
    private Attack[] _action = new Attack[2];
    private CameraController _camera;

    public int civiliansAlive;
    public float timeRemaining;

    private void OnEnable()
    {
        quitInput.Enable();
    }

    private void Disable()
    {
        quitInput.Disable();
    }

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
        _camera = Camera.main.GetComponent<CameraController>();
    }

    
    void Update()
    {
        if (quitInput.ReadValue<float>() > 0)
        { Application.Quit(); }
        switch(state)
        {
            case GameState.MainMenu:
                _message.text = "<size=60>Is It Big WEEVIL?</size>\n<size=30>Or Tiny People?</size>";
                _menuOverlay.enabled = true;
                _score.enabled = false;
                _instruction.SetActive(false);
                break;
            case GameState.Initialize:
                _message.enabled = false;
                _menuOverlay.enabled = false;
                _button.SetActive(false);
                _score.enabled = true;
                _camera.cameraState = CameraController.CameraStates.Following;
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
                    if (tempColour.a == 1) 
                    { 
                        tempColour.r = 0.5f;
                        tempColour.b = 0.5f;
                    }
                    else 
                    {
                        tempColour.r = 1f;
                        tempColour.b = 1f;
                    }
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
                state = GameState.GameOver;
                break;
            case GameState.GameOver:
                _message.text = "The radioactive juice ran out :(";
                _message.enabled = true;
                _menuOverlay.enabled = true;
                _button.SetActive(true);
                buttonText.text = "Ingest MORE!";
                break;
        }
    }
    public void Play()
    {
        if (state == GameState.GameOver)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            state = GameState.Initialize;
        }
       
    }
}
