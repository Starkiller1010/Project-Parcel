using UnityEngine;
using UnityEngine.SceneManagement;
public class GameState : MonoBehaviour
{
    MailSystem mailSystem = null;
    Flags gameFlags = null;

    void Start()
    {
        Director.InitDirector();
        GetMailSystem();
        GetGameFlags();
        TimeTracker timeTracker = Game.GET_TIME_TRACKER();
        timeTracker.GetTimer().StartTimer(timeTracker.GetTimer().GetTimeinSeconds());

    }
    
    void FixedUpdate()
    {
        Game.GET_TIME_TRACKER().GetTimer().tick();
    }
    
    public GameState(int dayCount, int[] addresses, int offset, string playTime, bool[,] flags = null)
    {
        mailSystem = new MailSystem(addresses);
        SetState(dayCount, addresses, offset, playTime, flags);
    }

    public MailSystem GetMailSystem()
    {
        if (mailSystem == null)
        {
            mailSystem = new MailSystem();
        }
        return mailSystem;
    }

    public Flags GetGameFlags()
    {
        if (gameFlags == null)
        {
            gameFlags = new Flags();
        }
        return gameFlags;
    }

    public void EndDay()
    {
        TimeTracker timeTracker = Game.GET_TIME_TRACKER();
        timeTracker.NextDay();
        Debug.Log("Day " + timeTracker.GetDay() + " has ended. Total play time: " + timeTracker.GetTimer().GetPlayTime());
        if (timeTracker.GetDay() > 7)
        {
            endGame();
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Game.GET_GAME_STATE().SetState(this);
            Director.InitDirector();
            Game.GET_PLAYER().GetControls().InitializeControls();
        }
    }

    public void SetState(GameState newState)
    {
        Debug.Log(newState.ToString());
        SetState(
            newState.GetDay(),
            newState.GetMailSystem().GetAllMailBoxAddresses(),
            newState.GetGameFlags().GetOffset(),
            newState.GetPlayTime(),
            newState.GetGameFlags().GetMarkers());
    }

    public void SetState(int dayCount, int[] addresses, int offset, string playTime, bool[,] flags = null)
    {
        SetDay(dayCount);
        SetTime(playTime);
        GetMailSystem().SetMailBoxAddresses(addresses);
        GetGameFlags().SetFlags(flags);
        GetGameFlags().SetOffset(offset);
    }

    private int GetDay()
    {
        TimeTracker timeTracker = Game.GET_TIME_TRACKER();
        return timeTracker.GetDay();
    }

    private string GetPlayTime()
    {
        TimeTracker timeTracker = Game.GET_TIME_TRACKER();
        return timeTracker.GetTimer().GetPlayTime();
    }

    private void SetTime(string playTime)
    {
        TimeTracker timeTracker = Game.GET_TIME_TRACKER();
        timeTracker.GetTimer().SetTimer(playTime);
    }

    private void SetDay(int dayCount)
    {
        TimeTracker timeTracker = Game.GET_TIME_TRACKER();
        timeTracker.SetDay(dayCount);
    }

    private void endGame()
    {
        string gameResult = string.Format("Congratulations! You have completed the game in {0} time.", Game.GET_TIME_TRACKER().GetTimer().GetPlayTime());
        Debug.Log(gameResult);
        // Additional end game logic can be added here
        Application.Quit(0);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
    
    public override string ToString()
    {
        return string.Format("Day: {0}, PlayTime: {1}, Mailboxes: {2}, Flags Offset: {3}", 
            GetDay(), 
            GetPlayTime(), 
            string.Join(", ", GetMailSystem().GetAllMailBoxAddresses()), 
            GetGameFlags().GetOffset());
    }
}
