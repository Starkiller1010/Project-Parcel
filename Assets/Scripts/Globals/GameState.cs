using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameState : MonoBehaviour
{
    MailSystem mailSystem = null;
    Flags gameFlags = null;
    List<Character> characters = null;

    void Start()
    {
        Director.InitDirector();
        GetGameFlags();
        TimeTracker timeTracker = Game.GET_TIME_TRACKER();
        timeTracker.GetTimer().StartTimer(timeTracker.GetTimer().GetTimeinSeconds());
        GetMailSystem().PopulateMailboxes(timeTracker.GetDay());
        GetCharacters();
    }

    void FixedUpdate()
    {
        Game.GET_TIME_TRACKER().GetTimer().tick();
    }

    public GameState(int dayCount, int[] addresses, int offset, string playTime, string[] characterNames)
    {
        mailSystem = new MailSystem(addresses);
        SetState(dayCount, addresses, offset, playTime, characterNames);
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

    public List<Character> GetCharacters()
    {
        if (characters == null)
        {
            SetCharacters(CharacterGenerator.generateCharacters(GetMailSystem().GetAllMailBoxAddresses()));
        }
        return characters;
    }

    public string[] GetCharacterNames()
    {
        if (characters != null)
        {
            List<string> names = new List<string>();
            foreach (Character character in characters)
            {
                names.Add(character.getName());
            }
            return names.ToArray();
        }
        return null;
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
            newState.GetCharacterNames());
    }

    public void SetState(int dayCount, int[] addresses, int offset, string playTime, string[] characterNames = null)
    {
        SetDay(dayCount);
        SetTime(playTime);
        GetMailSystem().SetMailBoxAddresses(addresses);
        // GetGameFlags().SetFlags(flags);
        GetGameFlags().SetOffset(offset);
        SetCharacters(CharacterGenerator.generateCharacters(addresses, characterNames));
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

    private void SetCharacters(List<Character> characters)
    {
        this.characters = characters;
    }

    private void endGame()
    {
        string gameResult = string.Format("Congratulations! You have completed the game in {0} time.", Game.GET_TIME_TRACKER().GetTimer().GetPlayTime());
        Debug.Log(gameResult);
        Application.Quit(0);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
    
    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("Day: " + GetDay());
        builder.Append("\nPlayTime: " + GetPlayTime());
        builder.Append("\nOffset: " + GetGameFlags().GetOffset());
        builder.Append("\nMailboxes: " + string.Join(",", GetMailSystem().GetAllMailBoxAddresses()));
        builder.Append("\nCharacters: " + string.Join(",", GetCharacterNames()));
        return builder.ToString();
    }
}
