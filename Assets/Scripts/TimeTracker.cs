using System;

public class TimeTracker
{
    private Timer timer;
    private int current_day;

    public TimeTracker()
    {
        timer = new Timer();
    }

    public string GetPlayTime()
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(timer.GetTimeinSeconds());
        string formattedTime = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
        return formattedTime;
    }

    public Timer GetTimer()
    {
        return timer;
    }

    public int GetDay()
    {
        return current_day;
    }

    public void SetDay(int day)
    {
        current_day = day;
    }

}