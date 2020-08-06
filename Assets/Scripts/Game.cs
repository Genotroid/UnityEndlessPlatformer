using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private LevelGenerator _levelGenerator;
    [SerializeField] private GameObject _buttonPanel;

    private void OnEnable()
    {
        _player.PlayerDied += GameOver;
    }

    private void OnDisable()
    {
        _player.PlayerDied -= GameOver;
    }

    private void GameOver()
    {
        Time.timeScale = 0;
        _buttonPanel.SetActive(true);
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        _player.ResetPlayer();
        _levelGenerator.enabled = true;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
