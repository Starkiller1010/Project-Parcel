using UnityEngine;

public static class Game
{
    private static GameState GAME_STATE = null;
    private static Player PLAYER = null;
    private static TimeTracker timeTracker = null;
    // TODO Setting Script should be init placed here

    [RuntimeInitializeOnLoadMethod]
    static void OnGameStart()
    {
        #if UNITY_EDITOR
        string fileName = FileManager.GetSavedGameStates()[0];
        GameState gameState = FileManager.LoadGameState("/" + fileName);
        GET_GAME_STATE().SetState(gameState);
        #endif
    }

    public static GameState GET_GAME_STATE()
    {
        if (GAME_STATE == null)
        {
            FindGameState();
        }
        return GAME_STATE;
    }

    public static Player GET_PLAYER()
    {
        if (PLAYER == null)
        {
            PLAYER = new Player();
        }
        return PLAYER;
    }
    
    public static TimeTracker GET_TIME_TRACKER()
    {
        if (timeTracker == null)
        {
            timeTracker = new TimeTracker();
        }
        return timeTracker;
    }

    public static void MAKE_GAME_STATE(int startDayCount = 0, string filename = null)
    {
        if (GAME_STATE == null) FindGameState();
        if (filename == null)
        {
            CreateNewGameState(startDayCount);
        } else
        {
            LoadGameState(filename);
        }
    }

    public static void RESET_GAME_STATE()
    {
        GAME_STATE = null;
    }
    
    private static void FindGameState()
    {
        GameObject gameObject = GameObject.FindGameObjectWithTag("GameState");
        GAME_STATE = gameObject.GetComponent<GameState>();
    }

    private static void LoadGameState(string filename)
    {
        if (GAME_STATE == null) FindGameState();
        GAME_STATE.SetState(FileManager.LoadGameState(filename));
    }

    private static void CreateNewGameState(int startDayCount)
    {
        if (GAME_STATE == null) FindGameState();
        GAME_STATE.SetState(startDayCount, null, MailSystem.GenerateOffset(), "00:00:00");
    }

    public static void LogError(string action, string method)
    {
        Debug.LogError($"Failed to {action} element in {method}.");
    }

    public static void FreezePlayer(bool freeze)
    {
        GET_PLAYER().GetControls().ToggleMovementState();
    }

    public static void END_GAME()
    {
        string gameResult = string.Format("Congratulations! You have completed the game in {0} time.", Game.GET_TIME_TRACKER().GetTimer().GetPlayTime());
        Debug.Log(gameResult);
        Application.Quit(0);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
