using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mailbox : MonoBehaviour
{
    private List<Letter> letters;
    public List<int> addresses; // Array to store the addresses of the characters associated with this mailbox  

    void Start()
    {
        // Initialize the mailbox with a random number of mails for the day
        letters = new List<Letter>(); // Initialize the letters list to store the mail for the day
        addresses = new List<int>(); // Initialize the addresses list to store the character addresses
    }

    public int GetMailCount()
    {
        return this.letters.Count;
    }

    public void ClearMail()
    {
        this.letters.Clear(); // Clear the letters list to remove all mail from the mailbox
    }

    public Letter[] GetLetters()
    {
        return this.letters.ToArray();
    }

    public void addAddress(int address)
    {
        if (!addresses.Contains(address))
        {
            addresses.Add(address); // Add the character's address to the mailbox's addresses list
        }
    }

    public void GenerateMail(Sprite Symbol, int address, string content = "Empty letter content")
    {
        Letter letter = new Letter(Symbol, address, content); // Create a new letter with the character's address and name
        this.letters.Add(letter); // Add the letter to the mailbox's letters list
    }
}
