using System;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public static event Action<int> OnScoreChanged;
    public TextMeshProUGUI scoreText;
    public int playerScore = 0;
    void Awake()
    {
        instance = this;
        UpdateUI();
    }

    public void AddScore(int amount) //점수 추가
    {
        playerScore += amount;
        UpdateUI();

        OnScoreChanged?.Invoke(playerScore);
    }
    void UpdateUI() //화면에 점수 표시
    {
        scoreText.text = "Score : " + playerScore;
    }
}
