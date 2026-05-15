using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class Workstation : MonoBehaviour
{
    private GUI ui;

    [SerializeField]
    public VisualTreeAsset mailButton;

    private MailSystem mailSystem = null;

    int rowCount = 5;

    private List<Button> letters;

    private LetterUI letterUI;
    private bool opened = false;

    void Start()
    {
        letters = new List<Button>();
        letterUI = GetComponent<LetterUI>();
        mailSystem = Game.GET_GAME_STATE().GetMailSystem();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && opened)
        {
            DisableWorkTable();
        }
    }

    private void CreateTable()
    {
        if (ui == null) 
            ui = new GUI(gameObject);
        ShowTable();
    }

    private void DestroyTable()
    {
        ui.DestroyGUI();
    }

    private void RemoveTable()
    {
        ui.RemoveClassFromColumn(GUI.Column.Middle, "table");
        RemoveLetters();
    }

    private void ShowTable()
    {
        ui.ChangeColumnWithClass(GUI.Column.Middle, "table");
        CheckLetters();
    }

    public void EnableWorkTable()
    {
        CreateTable();
        Game.GET_PLAYER().GetControls().ToggleMovementState(false);
        opened = true;
    }

    public void DisableWorkTable()
    {
        if (ui != null)
        {
            RemoveTable();
            letterUI.Close();
        }
        Game.GET_PLAYER().GetControls().ToggleMovementState(true);
        opened = false;
    }

    public void AddLetter(Letter letter)
    {
        int index = GetIndex();
        if (letters.Count < rowCount * 3)
        {
            Button letterElement = ui.AddTemplateToColumnWithRow<Button>(GUI.Column.Middle, mailButton, index);
            letterElement.text = mailSystem.GetCharacterAddress(letter.toIndex).ToString();
            letterElement.clicked += delegate { OpenLetter(letter); };
            letters.Add(letterElement);
        }

        if (index != GetIndex())
        {
            ui.AddRow(GUI.Column.Middle);
        }
    }

    private void OpenLetter(Letter letter)
    {
        Debug.Log(string.Format("Letter Opened: \n{0}", letter));
        RemoveTable();
        letterUI.Open(ui, letter);
    }

    private void CheckLetters()
    {
        List<Letter> playerLetters = Player.GetLetters();
        RemoveOutdatedGUILetters(playerLetters);
        AddMissingPlayerLetters(playerLetters);
    }

    private void AddMissingPlayerLetters(List<Letter> playerLetters)
    {
        foreach (Letter letter in playerLetters)
        {
            if (!letters.Exists(x => x.text == letter.toIndex.ToString()))
            {
                AddLetter(letter);
            }
        }
    }
    
    private void RemoveOutdatedGUILetters(List<Letter> playerLetters)
    {
        foreach (Button element in letters)
            {
                if (!playerLetters.Exists(x => x.toIndex.ToString() == element.text))
                {
                    RemoveLetter(element);
                }
            }
    }

    private int GetIndex()
    {
        int index = 0;
        if (letters.Count >= rowCount)
        {
            index++;
            if (letters.Count >= rowCount * 2)
                index++;
        }
        return index;
    }

    private void RemoveLetter(VisualElement letter)
    {
        letter.RemoveFromHierarchy();
    }
    
    private void RemoveLetters()
    {
        foreach (VisualElement element in letters)
        {
            RemoveLetter(element);
        }
        ClearLetters();
    }

    private void ClearLetters()
    {
        letters.Clear();
    }
}
