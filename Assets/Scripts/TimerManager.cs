using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    public static TimerManager instance;

    public TextMeshProUGUI timerTMP;
    private float startTime = 0f;
    private float pausedTime = 0f;
    private bool isRunning = false;

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable() //점수 받기
    {
        ScoreManager.OnScoreChanged += HandleScoreChanged;
    }

    private void OnDisable()
    {
        ScoreManager.OnScoreChanged -= HandleScoreChanged;
    }

    private void HandleScoreChanged(int score)
    {
        Debug.Log("점수 추가");
        //if (score >= 70) 
        //{
        //    LoadHappyEndingScene();
        //}
    }
    void Update()
    {
        if (isRunning)
        {
            float elapsedTime = Time.time - startTime + pausedTime;

            if(elapsedTime >= 600f)
            {
                isRunning = false;
                LoadBadEndingScene();
            }

            int min = Mathf.FloorToInt(elapsedTime / 60);
            int sec = Mathf.FloorToInt(elapsedTime % 60);
            int millsec = Mathf.FloorToInt((elapsedTime * 1000) % 1000);
            
            if(timerTMP != null)
                timerTMP.text = $"{min:00}:{sec:00}:{millsec:000}";
        }
    }

    public void StartTimer() //타이머 시작
    {
        if (isRunning) return;
        startTime = Time.time;
        isRunning = true;
        Debug.Log("타이머 시작: " + startTime);
    }

    public void PauseTimer() //타이머 일시정지
    {
        if (!isRunning) return;
        pausedTime += Time.time - startTime;
        isRunning = false;
    }

    public void ResumeTimer() //타이머 재개
    {
        if (isRunning) return;

        startTime = Time.time;
        isRunning = true;
    }

    void LoadHappyEndingScene()
    {
        SceneManager.LoadScene("HappyEnding");
    }

    void LoadBadEndingScene()
    {
        SceneManager.LoadScene("BadEnding");
    }

}
