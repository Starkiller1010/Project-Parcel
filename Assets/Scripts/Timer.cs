using System;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private int curr_time = 0;
    private bool isPaused = true;

    public Timer() {}

    public void StartTimer(int time = 0)
    {
        this.curr_time = time;
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
        TimeSpan timeSpan = TimeSpan.FromSeconds(curr_time);
        string formattedTime = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
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

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isPaused)
        {
            curr_time++;
        }

    }
}
