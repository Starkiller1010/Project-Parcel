using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class Game
{
    private static GameState2 GAME_STATE = null;
    private static Player PLAYER = null;


    public static GameState2 GET_GAME_STATE()
    {
        if (GAME_STATE == null)
        {
            FindGameState();
        }
        return GAME_STATE;
    }

    public static Player GET_PLAYER()
    {   
        return PLAYER;
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
        GAME_STATE = gameObject.GetComponent<GameState2>();
    }

    private static void LoadGameState(string filename)
    {
        if (GAME_STATE == null) FindGameState();
        GAME_STATE.SetState(FileManager.LoadGameState(filename));
    }

    private static void CreateNewGameState(int startDayCount)
    {
        if (GAME_STATE == null) FindGameState();
        GAME_STATE.SetState(startDayCount, null, 0, null, "00:00:00");
    }
}
