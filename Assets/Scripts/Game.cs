using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class Game
{
    private static GameState GAME_STATE = null;
    private static Player PLAYER = null;
    private static Director DIRECTOR = new Director();
    // TODO Setting Script should be init placed here
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
            PLAYER = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }
        return PLAYER;
    }
    
    public static Director GET_DIRECTOR()
    {
        return DIRECTOR;
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
}
