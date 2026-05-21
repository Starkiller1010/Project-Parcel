using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryUI
{
    private GUI ui;
    private VisualElement modal;
    private Mailbox mailbox;
    public bool isOpen = false;
    private Button escapeButton;

    public void Open(Mailbox mailbox)
    {
        ui = new GUI();
        this.mailbox = mailbox;
        // modal = ui.AddTemplateToColumn<VisualElement>(GUI.Column.Middle, "Inventory");
        modal = ui.ChangeColumnWithClass(GUI.Column.Middle, "table");
        Game.GET_PLAYER().GetControls().ToggleMovementState(false);
        AddPlayerLetters();
        AddEscapeButton();
        isOpen = true;
    }

    public void Close()
    {
        if (ui != null)
        {
            ui.RemoveClassFromColumn(GUI.Column.Middle, "table");
            RemovePlayerLetters();
            Game.GET_PLAYER().GetControls().ToggleMovementState(true);
            isOpen = false;
            ui = null;
            mailbox = null;
        }
    }

    private void AddEscapeButton()
    {
        escapeButton = new Button();
        escapeButton.clicked += delegate { Close(); RemoveEscapeButton(); };
        escapeButton.text = "Cancel Delivery";
        ui.AddElement(GUI.Column.Middle, escapeButton);
    }

    private void RemoveEscapeButton()
    {
        escapeButton.RemoveFromHierarchy();
    }

    private void AddPlayerLetters()
    {
        List<Letter> playerLetters = Player.GetLetters();
        foreach (Letter letter in playerLetters)
        {
            AddLetter(letter);
        }
    }

    private void RemovePlayerLetters()
    {
        List<Letter> playerLetters = Player.GetLetters();
        foreach (Letter letter in playerLetters)
        {
            RemoveLetter(Game.GET_GAME_STATE().GetMailSystem().GetCharacterAddress(letter.toIndex).ToString());
        }
    }

    private void AddLetter(Letter letter)
    {
        int count = 0, rowCount = 1;
        int index = GetIndex(count, rowCount);
        VisualTreeAsset mailButton = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/UI/uxml/mail-button.uxml");
        if (count < rowCount * 3)
        {
            Button letterElement = ui.AddTemplateToColumnWithRow<Button>(GUI.Column.Middle, mailButton, index);
            letterElement.text = letter.GetAddress().ToString();
            letterElement.clicked += delegate { DeliverMail(letter); };
            letterElement.name = letterElement.text;
            count++;
        }

        if (index != GetIndex(count, rowCount))
        {
            ui.AddRow(GUI.Column.Middle);
        }
    }

    private void RemoveLetter(string toAddress)
    {
        Button letter = modal.Query<Button>().Where(elem => elem.text == toAddress).First();
        if (letter != null) letter.RemoveFromHierarchy();
    }

    private void DeliverMail(Letter letter)
    {
        Debug.Log(string.Format("Addresses: {0}\nLetter Addressed To: {1}", string.Join(",", mailbox.addresses.ToArray()), letter.GetAddress()));
        if (mailbox.addresses.Exists(x => x == letter.GetAddress()))
        {
            Character character = Game.GET_GAME_STATE().GetCharacters().Find(x => x.getAddress() == letter.GetAddress());
            List<int?> letters = new List<int?>();
            letters.AddRange(character.getLetters());
            letters.Add(letter.UID);
            character.setLetters(letters);
            RemoveLetter(letter.GetAddress().ToString());
            RemoveEscapeButton();
            Close();
        } else
        {
            Debug.Log("Letter is not meant for this mailbox");
        }
    }
    
    private int GetIndex(int count, int rowCount)
    {
        int index = 0;
        if (count >= rowCount)
        {
            index++;
            if (count >= rowCount * 2)
                index++;
        }
        return index;
    }

}