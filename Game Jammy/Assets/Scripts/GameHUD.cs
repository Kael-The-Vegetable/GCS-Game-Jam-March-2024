using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameHUD : MonoBehaviour
{
    public enum GameState
    {
        MainMenu,
        Playing,
        GameOver
    }
    private GameState _state;
    private TextMeshProUGUI _message;
    void Awake()
    {
        _state = GameState.MainMenu;
        _message = FindObjectOfType<TextMeshProUGUI>();
    }

    
    void Update()
    {
        switch(_state)
        {
            case GameState.MainMenu:
                _message.text = "<size=60>Is it Big WEEVIL?</size>\n<size=20>or small peeople?</size>";
                break;
            case GameState.Playing:

                break;
            case GameState.GameOver:

                break;
        }
    }
}
