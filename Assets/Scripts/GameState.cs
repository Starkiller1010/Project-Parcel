using UnityEngine;
public class GameState : MonoBehaviour
{
    TimeTracker timeTracker = new TimeTracker();
    MailSystem mailSystem = null;
    Flags gameFlags = new Flags();
    Director director = new Director();
    
    public GameState()
    {
        mailSystem = new MailSystem(new int[5] { 0, 1, 2, 3, 4 });
    }

    public GameState(int dayCount, int[] addresses, int offset, string playTime, bool[,] flags = null)
    {
        mailSystem = new MailSystem(addresses);
        SetState(dayCount, addresses, offset, playTime, flags);
    }

    public TimeTracker GetTimeTracker()
    {
        return timeTracker;
    }

    public MailSystem GetMailSystem()
    {
        return mailSystem;
    }

    public Flags GetGameFlags()
    {
        return gameFlags;
    }

    public void SetState(GameState newState)
    {
        SetState(
            newState.timeTracker.GetDay(),
            newState.mailSystem.GetAllMailBoxAddresses(),
            newState.gameFlags.GetOffset(),
            newState.timeTracker.GetTimer().GetPlayTime(),
            newState.gameFlags.GetMarkers());
    }

    public void SetState(int dayCount, int[] addresses, int offset, string playTime, bool[,] flags = null)
    {
        timeTracker.SetDay(dayCount);
        timeTracker.GetTimer().SetTimer(playTime);
        mailSystem.SetMailBoxAddresses(addresses);
        gameFlags.SetFlags(flags);
        gameFlags.SetOffset(offset);
    }

    private void endGame()
    {
        string gameResult = string.Format("Congratulations! You have completed the game in {0} days and {1} time.", timeTracker.GetDay(), timeTracker.GetTimer().GetPlayTime());
        Debug.Log(gameResult);
        // Additional end game logic can be added here
        Application.Quit(0);
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
