using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectiveManager : MonoBehaviour
{
    public static ObjectiveManager instance;
    [SerializeField] private float startTime = 60f; // Starting time in seconds
    [SerializeField] private bool countDown = true; // True for countdown, false for count up
    [SerializeField] private TextMeshProUGUI timerText; // Optional UI text to display the timer
    [SerializeField] private bool autoStart = true; // Automatically start the timer
    [SerializeField]
    private PlayerHealth health;
    private float currentTime;
    public bool isRunning = false;
    public bool KillGameMode = false;
    public delegate void TimerEvent();
    public event TimerEvent OnTimerFinish;

   
    private void Awake()
    {
        if(instance == null)
            instance = this;
    }
    private void Start()
    {
        ResetTimer();

        if (autoStart)
        {
            StartTimer();
        }
    }
    private void Update()
    {
        if (isRunning)
        {
            UpdateTimer();
        }
    }
    private void UpdateTimer()
    {
        if (countDown)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                if(KillGameMode)
                {
                    if(KillCountManager.instance.KillCount >= KillCountManager.instance.RequiredKills)
                    {
                        currentTime = 0;
                        StopTimer();
                        PerformOnTimerEnd();
                        OnTimerFinish?.Invoke();
                    }
                    else if(KillCountManager.instance.KillCount < KillCountManager.instance.RequiredKills)
                    {
                        health.HandleDeath();
                        StopTimer();
                    }
                }
                else
                {
                    currentTime = 0;
                    StopTimer();
                    PerformOnTimerEnd();
                    OnTimerFinish?.Invoke();
                }
                
            }
        }
        else
        {
            currentTime += Time.deltaTime;
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        if (timerText != null)
        {
            timerText.text = FormatTime(currentTime);
        }
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void StartTimer()
    {
        isRunning = true;
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    public void ResetTimer()
    {
        currentTime = countDown ? startTime : 0;
        UpdateUI();
    }

    public float GetCurrentTime()
    {
        return currentTime;
    }
    private void PerformOnTimerEnd()
    {
        
        Debug.Log("Timer has reached 0! Perform the desired behavior here.");
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor
        Cursor.visible = true;
        MenuManager.instance.ProceedToAnotherLevel();

    }
}
