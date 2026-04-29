using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;
public class GameState : MonoBehaviour
{
    private static int dayCount = 0;
    private string currentDay = "Day " + dayCount;
    public Confirmation confirmationPanel = null;
    private Player player = null;
    public Text text;
    public Timer timer = null;
    public Character[] characters;
    private GameObject[] mailboxes;
    private bool[,] flags = new bool[8, 5];
    public int offset;
    public int[] addresses = null;

    // Start is called before the first frame update
    // void Start()
    // {
    //     textPanel = GameObject.FindGameObjectWithTag("DialogueBox");
    //     confirmationPanel = GameObject.FindGameObjectWithTag("ConfirmationBox").GetComponent<Confirmation>();
    //     player = this.GetComponent<Player>();
    //     mailboxes = GameObject.FindGameObjectsWithTag("Mail Container");
    //     confirmationPanel.gameObject.SetActive(false);
    //     if (this.text == null)
    //     {
    //         Debug.LogError("GameState script requires a Text component on the same GameObject.");
    //     }
    //     else
    //     {
    //         this.text.text = currentDay;
    //     }

    //     if (textPanel != null)
    //     {
    //         textPanel.SetActive(false);

    //     }
    //     else
    //     {
    //         Debug.LogError("Panel GameObject with name 'Dialogue Box' not found in the scene.");
    //     }
    //     if (confirmationPanel == null)
    //     {
    //         Debug.LogError("Panel GameObject with name 'Confirmation Box' not found in the scene.");
    //     }
    //     if (player == null)
    //     {
    //         Debug.LogError("Player component not found on GameObject with tag 'Player'.");
    //     }
    //     timer = GetTimer();
    //     timer.ResumeTimer();
    // }

    // void FixedUpdate()
    // {
    //     InitGame();
    // }

    void Start()
    {
        InitGame();
    }

    private void initCharacters()
    {
        characters = new Character[8];
    }

    public void InitGame()
    {
        string[] objects = { "DialogueBox", "ConfirmationBox" };
        foreach(string obj in objects)
        {
            SetObjectToInactive(obj);
        }
        GetTimer();
        CharacterGenerator.generateCharacters(this);
    }
    
    private void SetObjectToInactive(string objectName)
    {
        GameObject gameObject = GameObject.FindGameObjectWithTag(objectName);
        if (gameObject != null) { gameObject.SetActive(false); }
    }

    public GameState(GameState gameState)
    {
        this.SetDayCount(gameState.GetDayCount());
        this.confirmationPanel = gameState.confirmationPanel;
        this.player = gameState.GetPlayer();
        this.text = gameState.text;
        this.timer = gameState.timer;
        this.characters = gameState.characters;
        this.mailboxes = gameState.mailboxes;
        this.flags = gameState.flags;
    }

    public GameState(int dayCount, int[] addresses, int offset, bool[,] flags, string timer)
    {
        this.SetDayCount(dayCount);
        this.SetAddresses(addresses);
        this.SetFlags(flags);
        this.SetOffset(offset);
    }

    public void SetState(int dayCount, int[] addresses, int offset, bool[,] flags, string timer)
    {
        this.SetDayCount(dayCount);
        this.SetAddresses(addresses);
        this.SetFlags(flags);
        this.SetOffset(offset);
    }

    public void SetState(GameState _state)
    {
        this.SetDayCount(_state.GetDayCount());
        this.confirmationPanel = _state.confirmationPanel;
        this.player = _state.GetPlayer();
        this.text = _state.text;
        this.timer = _state.timer;
        this.characters = _state.characters;
        this.mailboxes = _state.mailboxes;
        this.flags = _state.flags;
    }

    public bool[,] getFlags()
    {
        return flags;
    }

    public void SetFlags(bool[,] _flags)
    {
        this.flags = _flags;
    }

    public void SetOffset(int _offset)
    {
        this.offset = _offset;
    }

    public GameState CreateGame()
    {
        // Initialize game state, characters, mailboxes, etc. here
        // instance = this;
        // CharacterGenerator.generateCharacters(this);
        this.offset = assignCharactersToMailboxes();
        timer.StartTimer();
        SaveGame(this.offset);
        return this;
    }

    public void SaveGame(int _offset)
    {
        FileManager.SaveGameState(this, this.offset);
    }
    
    public Timer GetTimer()
    {
        if (timer == null)
        {
            this.gameObject.AddComponent<Timer>();
           timer = this.GetComponent<Timer>();
        }
        return timer;
    }

    public void addCharacter(Character character)
    {
        if (character == null)
        {
            Debug.LogError("Cannot add a null character to the game state.");
            return;
        } else if(characters == null || characters.Length == 0)
        {
            initCharacters();
        }

        for (int i = 0; i < characters.Length; i++)
        {
            if (characters[i] == null)
            {
                characters[i] = character;
                return;
            } else
            {
                Debug.Log(characters[i].getName() + " is at: " + i);
            }
        }
        Debug.LogError("Character array is full. Cannot add more characters to the game state.");
    }

    public Player GetPlayer()
    {
        return player;
    }

    public string GetCurrentDay()
    {
        return currentDay;
    }

    public int GetDayCount()
    {
        return dayCount;
    }

    public int[] GetAllAddresses()
    {
        if (this == null || this.characters == null || this.characters.Length <= 0)
        {
            Debug.LogError("Character Addresses are empty");
            return new int[0];
        }
        int[] output = new int[this.characters.Length];
        int index = 0;
        foreach (Character character in this.characters)
        {
            output[index] = character.getAddress();
            index++;
        }
        return output;
    }

    public void SetAddresses(int[] _addresses)
    {
        GameState game = Game.GetGameState();
        if (_addresses == null || _addresses.Length == 0) {
            Debug.Log("Leaving because input was null or empty");
            return;}
        game.addresses = new int[_addresses.Length];
        int index = 0;
        foreach(int address in _addresses)
        {
            game.addresses[index] = address;
            Debug.Log(game.addresses[index]);
            index++;
        }
        // this.addresses = _addresses;
    }

    public void reloadScene()
    {
        // Get the build index of the currently active scene
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Load the scene using its build index
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void StartNewDay()
    {
        dayCount++;
        currentDay = "Day " + dayCount;
        if (dayCount >= 8)
        {
            endGame();
        }
        else
        {
            SceneManager.LoadScene("WorkroomScene");
        }

        if (this.text != null)
        {
            this.text.text = currentDay;
        }
    }

    public void SetDayCount(int newDayCount)
    {
        dayCount = newDayCount;
        currentDay = "Day " + dayCount;
        if (this.text != null)
        {
            this.text.text = currentDay;
        }
    }

    public void ShowConfirmationPanel(string promptText)
    {
        if (confirmationPanel != null)
        {
            confirmationPanel.gameObject.SetActive(true);
            confirmationPanel.SetPromptText(promptText);
        }
        else
        {
            Debug.LogError("Panel GameObject with name 'Confirmation Box' not found in the scene.");
        }
    }

    public void HideConfirmationPanel()
    {
        if (confirmationPanel != null)
        {
            confirmationPanel.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("Panel GameObject with name 'Confirmation Box' not found in the scene.");
        }
    }

    public int assignCharactersToMailboxes(int offset = -1)
    {
        // Initialize game state, characters, mailboxes, etc. here
        int mailboxIndex = offset;
        if (offset < 0)
        {
            mailboxIndex = Random.Range(0, mailboxes.Length); // Start with a random mailbox index
        }
        foreach (string character in CharacterGenerator.getCharacterNames())
        {
            Mailbox mailbox = mailboxes[mailboxIndex].GetComponent<Mailbox>();
            if (mailbox == null)
            {
                Debug.LogError("Mailbox component not found on GameObject with tag 'Mail Container'.");
                continue; // Skip this mailbox if it doesn't have a Mailbox component
            }
            int characterIndex = System.Array.IndexOf(CharacterGenerator.getCharacterNames(), character);
            switch (character)
            {
                case "Diplomat":
                    mailbox.addresses.Add(characters[characterIndex].getAddress()); // Add the character's address to the corresponding mailbox's addresses list    
                    mailbox.addresses.Add(characters[characterIndex + 1].getAddress()); // Add the character's address to the corresponding mailbox's addresses list
                    break;
                case "Mistress":
                    mailbox.addresses.Add(characters[characterIndex].getAddress()); // Add the character's address to the corresponding mailbox's addresses list    
                    mailbox.addresses.Add(characters[characterIndex + 1].getAddress()); // Add the character's address to the corresponding mailbox's addresses list
                    break;
                case "Doctor":
                    mailbox.addresses.Add(characters[characterIndex].getAddress()); // Add the character's address to the corresponding mailbox's addresses list    
                    mailbox.addresses.Add(characters[characterIndex + 1].getAddress()); // Add the character's address to the corresponding mailbox's addresses list
                    break;
                case "Cult Leader":
                    mailbox.addresses.Add(characters[characterIndex].getAddress()); // Add the character's address to the corresponding mailbox's addresses list    
                    mailbox.addresses.Add(characters[characterIndex + 1].getAddress()); // Add the character's address to the corresponding mailbox's addresses list
                    break;
                default:
                    mailboxIndex = (mailboxIndex + 1) % mailboxes.Length; // Move to the next mailbox for the next character
                    continue; // Skip generating mail for unknown characters
            }
        }
        return mailboxIndex;
    }

    public void SetCharacters(int[] characterAddresses)
    {
        // if (characters.Length != 0) characters = new Character[characters.Length];
        // else characters = new Character[CharacterGenerator.getCharacterNames().Length];
        // CharacterGenerator.generateCharacters(this);
       for (int i = 0; i < CharacterGenerator.getCharacterNames().Length; i++)
        {
            characters[i].setAddress(characterAddresses[i]);
        }
    }

    private void generateMail(Mailbox mailbox, int address, Sprite symbol)
    {
        mailbox.GenerateMail(symbol, address);
    }
    
    private void endGame()
    {
        Debug.Log("Game Over! You have completed the game in " + dayCount + " days.");
        // Additional end game logic can be added here
        Application.Quit(0);

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
    
    
    
}
