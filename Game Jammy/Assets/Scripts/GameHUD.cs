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
        GameOver
    }
    public GameState state { get; private set; }
    private TextMeshProUGUI _message;
    private Image _menuOverlay;
    private GameObject _button;
    private TextMeshProUGUI _buttonText;
    void Awake()
    {
        state = GameState.MainMenu;
        _message = GameObject.Find("Message").GetComponent<TextMeshProUGUI>();
        _menuOverlay = GameObject.Find("MenuOverlay").GetComponent<Image>();
        _button = GameObject.Find("Button");
        _buttonText = _button.GetComponentInChildren<TextMeshProUGUI>();
    }

    
    void Update()
    {
        switch(state)
        {
            case GameState.MainMenu:
                _message.text = "<size=60>Is it Big WEEVIL?</size>\n<size=20>or smol ppl?</size>";
                _menuOverlay.enabled = true;
                break;
            case GameState.Initialize:
                _message.enabled = false;
                _menuOverlay.enabled = false;
                _button.SetActive(false);
                state = GameState.Playing;
                break;
            case GameState.Playing:
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
