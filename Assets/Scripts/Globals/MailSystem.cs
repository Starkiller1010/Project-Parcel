using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;


public class MailSystem
{
    private List<Mailbox> mailboxes = new List<Mailbox>();
    private int offset = 0;

    private Dictionary<int, List<LetterProbabilityTable>> LetterDeliveryTable = new Dictionary<int, List<LetterProbabilityTable>>(); 

    public MailSystem()
    {
        FindAllMailBoxes();
    }

    public MailSystem(int[] character_addresses)
    {
        FindAllMailBoxes();
        SetMailBoxAddresses(character_addresses);
        offset = Game.GET_GAME_STATE().GetGameFlags().GetOffset();
    }

    public static int GenerateOffset()
    {
        return Random.Range(0, 5);
    }

    public static int[] GenerateCharacterAddresses(int mailboxCount)
    {
        List<int> addresses = new List<int>();
        for (int i = 0; i < mailboxCount * 2; i++) // Assuming each mailbox has 2 addresses
        {
            int address = GenerateCharacterAddress(); // Generate a random address between 1000 and 9999
            while (addresses.Contains(address)) // Ensure the address is unique
            {
                address = GenerateCharacterAddress();
            }
            addresses.Add(address);
        }
        return addresses.ToArray();
    }

    public void FindAllMailBoxes()
    {
        GameObject[] boxes = GameObject.FindGameObjectsWithTag("Mail Container");
        foreach (GameObject box in boxes) mailboxes.Add(box.GetComponent<Mailbox>());
    }

    public void ClearAllMail()
    {
        foreach (Mailbox mailbox in mailboxes)
        {
            mailbox.ClearMail(); // Clear the mail from each mailbox
        }
    }

    public Mailbox GetMailBox(int index)
    {
        Mailbox mailbox = null;
        if (index >= 0 && index < mailboxes.Count)
        {
            mailbox = mailboxes[index];
        }
        else
        {
            Debug.LogError("Invalid mailbox index: " + index);
        }
        return mailbox;
    }

    public int[] GetAllMailBoxAddresses()
    {
        List<int> addresses = new List<int>();
        foreach (Mailbox mailbox in mailboxes)
        {
            addresses.AddRange(mailbox.addresses); // Add the addresses from each mailbox to the list
        }
        return addresses.ToArray();
    }

    public void SetMailBoxAddresses()
    {
        if (mailboxes.Count == 0)
        {
            Debug.LogError("No mailboxes found in the scene.");
        }
        else
        {
            foreach (Mailbox mailbox in mailboxes)
            {
                mailbox.ClearAddresses();
                mailbox.addAddress(GenerateCharacterAddress());
                mailbox.addAddress(GenerateCharacterAddress());
            }
        }
    }

    public void SetMailBoxAddresses(int[] _addresses)
    {
        if (mailboxes.Count == 0)
        {
            Debug.LogError("No mailboxes found in the scene.");
        }
        else
        {
            int index = 0;
            foreach (Mailbox mailbox in mailboxes)
            {
                mailbox.ClearAddresses();
                if (index < _addresses.Length)
                {
                    mailbox.addAddress(_addresses[index]);
                    index++;
                    mailbox.addAddress(_addresses[index]);
                    index++;
                }
            }
        }
    }

    public void PopulateMailboxes(int dayIndex)
    {
        List<Letter> letters = GenerateMail(dayIndex);
        if (mailboxes == null) FindAllMailBoxes();
        foreach(Letter letter in letters)
        {
            int index = CalculateMailbox(letter.toIndex);
            Debug.Log(string.Format("Populating mailbox {0}", index));
            Mailbox mailbox = mailboxes.ToArray()[index];
            if (mailbox != null)
            {
                mailbox.GenerateMail(letter);
            }
        }
    }

    private List<Letter> GenerateMail(int dayIndex)
    {
        List<Letter> letters = new List<Letter>();
        List<Letter> allLetters = FileManager.GetLetters();
        List<Probability> letterChances = FileManager.LoadDayTable().days[dayIndex].probabilities.letterProbabilities;
        foreach (Probability chance in letterChances)
        {
            Letter letter = allLetters.Find(x => x.GetUID() == chance.UID);
            if (letter != null && CheckRequirements(letter))
            {
                letters.Add(letter);
            }
        }
        return letters;
    }
    
    private bool CheckRequirements(Letter letter)
    {
        foreach(int requirement in letter.requirements)
        {
            if (!Game.GET_GAME_STATE().GetGameFlags().GetCompletedLetters().ContainsKey(requirement))
                return false;
        }
        return true;
    }
    
    private int CalculateMailbox(int toIndex)
    {
        int mailBoxIndex = Math.Abs(toIndex / 2);
        return 0 + mailBoxIndex;
    }

    private static int GenerateCharacterAddress()
    {
        return Random.Range(1000, 9999);
    }
    
    struct LetterProbabilityTable {
        string id;
        float probability;
    }

}
