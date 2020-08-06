using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _player.CoinTaked += ChangeScoreText;
    }

    private void OnDisable()
    {
        _player.CoinTaked -= ChangeScoreText;
    }

    private void ChangeScoreText(int coinsCount)
    {
        _scoreText.text = coinsCount.ToString();
    }
}
