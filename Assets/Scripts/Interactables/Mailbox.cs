using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Mailbox : MonoBehaviour
{
    public List<Letter> letters = new List<Letter>();
    public List<int> addresses = new List<int>(); // Array to store the addresses of the characters associated with this mailbox
    [SerializeField]
    public VisualTreeAsset mailboxMenu;
    private GUI ui;
    private VisualElement menuInstance;
    private InventoryUI inventoryUI;
    public bool isUIOpen = false;

    void Start()
    {
        inventoryUI = new InventoryUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseMailboxMenu();
        }
    }

    public void OpenMailboxMenu()
    {
        if (mailboxMenu != null)
        {
            isUIOpen = true;
            if (ui == null)
                ui = new GUI();
            menuInstance = ui.AddTemplateToColumn<VisualElement>(GUI.Column.Middle, mailboxMenu);
            menuInstance.Q(className: "modal").Q<Button>(className: "confirm").clicked += delegate { CloseMailboxMenu(); inventoryUI.Open(this); Debug.Log(string.Join(",", addresses)); };
            menuInstance.Q(className: "modal").Q<Button>(className: "cancel").clicked += CloseMailboxMenu;
            Game.GET_PLAYER().GetControls().ToggleMovementState(false);
        }
        else
        {
            Debug.LogError("Mailbox menu VisualTreeAsset is not assigned.");
        }
    }

    public void CloseMailboxMenu()
    {
        if (ui != null)
        {
            menuInstance.RemoveFromHierarchy(); // Remove the menu from the UI hierarchy
            isUIOpen = false;
            Game.GET_PLAYER().GetControls().ToggleMovementState(true);
        }
    }

    public int GetMailCount()
    {
        return this.letters.Count;
    }

    public void ClearMail()
    {
        this.letters.Clear(); // Clear the letters list to remove all mail from the mailbox
        GetComponent<SpriteRenderer>().color = Color.red; // Change the mailbox's color to red to indicate it is empty
    }

    public Letter[] GetLetters()
    {
        if (letters == null) {
            letters = new List<Letter>();
        }
        return this.letters.ToArray();
    }

    public void addAddress(int address)
    {
        if (!addresses.Contains(address))
        {
            addresses.Add(address); // Add the character's address to the mailbox's addresses list
        }
    }

    public void ClearAddresses()
    {
        this.addresses.Clear(); // Clear the addresses list to remove all character associations from the mailbox
    }

    public void GenerateMail(Letter letter)
    {
        this.letters.Add(letter); // Add the letter to the mailbox's letters list
        GetComponent<SpriteRenderer>().color = Color.green; // Change the mailbox's color to green to indicate it has mail
    }
}
