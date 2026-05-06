using System;
using UnityEngine;

public class Timer
{
    private int curr_time = 0;
    private int minutes = 0;
    private int hours = 0;
    private int milliseconds = 0;
    private int seconds = 0;
    private bool isPaused = true;

    public Timer() {}

    public void StartTimer(int startTime = 0)
    {
        this.curr_time = startTime;
        isPaused = false;
    }

    public void ToggleTimer()
    {
        isPaused = !isPaused;
    }

    public int GetTimeinSeconds()
    {
        return curr_time;
    }

    public string GetPlayTime()
    {
        string formattedTime = string.Format("{0:D2}:{1:D2}:{2:D2}", hours, minutes, seconds);
        return formattedTime;
    }

    public void SetTimer(string newTime)
    {
        string[] parts = newTime.Split(':');
        if (parts.Length != 3 || !int.TryParse(parts[0], out _) || !int.TryParse(parts[1], out _) || !int.TryParse(parts[2], out _))
        {
            Debug.LogError("Invalid time format. Expected HH:MM:SS.");
            return;
        }
        else
        {
            int hours = int.Parse(parts[0]);
            int minutes = int.Parse(parts[1]);
            int seconds = int.Parse(parts[2]);
            this.curr_time = hours * 3600 + minutes * 60 + seconds;
        }
    }

    public void SetTimer(int newTime)
    {
        this.curr_time = newTime;
    }

    private void ResetTimer()
    {
        this.curr_time = 0;
        this.minutes = 0;
        this.hours = 0;
        this.seconds = 0;
    }

    public void StopTimer()
    {
        isPaused = true;
        ResetTimer();
    }

    public void PauseTimer()
    {
        isPaused = true;
    }

    public bool IsTimerRunning()
    {
        return !isPaused;
    }

    private void CheckMinutes()
    {
        if (seconds >= 60)
        {
            seconds = 0;
            minutes++;
        }
    }

    private void CheckHours()
    {
        if (minutes >= 60)
        {
            minutes = 0;
            hours++;
        }
    }

    private void CheckSeconds()
    {
        if (milliseconds >= 100)
        {
            milliseconds -= 100;
            seconds++;
        }
    }

    public void tick()
    {
        // Debug.Log("Current Time: " + GetPlayTime());
        curr_time++;
        if (!isPaused)
        {
            milliseconds++;
            CheckSeconds();
            CheckMinutes();
            CheckHours();
        }
    }
}
