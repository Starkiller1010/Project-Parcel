using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UIElements;
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

    void OnEnable()
    {
        ui = new GUI(gameObject);
        letters = new List<Button>();
        mailSystem = Game.GET_GAME_STATE().GetMailSystem();
        ui.MakeTable();
        ui.HideGUI();
    }

    public void EnableWorkTable()
    {
        CheckLetters();
        ui.ShowGUI();
    }

    public void DisableWorkTable()
    {
        // RemoveLetters();
        ui.HideGUI();
    }

    public void AddLetter(Letter letter)
    {
        int index = GetIndex();
        if (letters.Count < rowCount * 3)
        {
            Button letterElement = ui.AddTemplateToColumn<Button>(GUI.Column.Middle, mailButton, index);
            //index needs to be generated from letter toIndex
            letterElement.text = mailSystem.GetCharacterAddress(letter.toIndex).ToString();
            // letterElement.RegisterCallback<OnButtonClick>(letter.Deliver);
            // letterElement.clicked += OpenLetter;
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
        Debug.Log(string.Format("Letter Opened: \n{0}", letter ));
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
        // ui.RemoveElementFromRow(GUI.Column.Middle, letter, GetIndex());
        letter.RemoveFromHierarchy();
    }
    
    private void RemoveLetters()
    {
        letters.Clear();
    }
}
