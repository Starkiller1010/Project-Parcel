using System.Collections.Generic;
using UnityEngine;

public class Mailbox : MonoBehaviour
{
    public List<Letter> letters = new List<Letter>();
    public List<int> addresses = new List<int>(); // Array to store the addresses of the characters associated with this mailbox  

    void Start()
    {

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
    }
}
