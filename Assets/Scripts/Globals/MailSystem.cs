using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;


public class MailSystem
{
    private static List<Mailbox> mailboxes = new List<Mailbox>();
    private int offset = 0;
    private static List<int> AllAddresses = new List<int>();
    private Dictionary<int, List<LetterProbabilityTable>> LetterDeliveryTable = new Dictionary<int, List<LetterProbabilityTable>>(); 

    public MailSystem()
    {
        FindAllMailBoxes();
    }

    public MailSystem(int[] character_addresses)
    {
        FindAllMailBoxes();
        SetMailBoxAddresses(character_addresses);
        SetOffset();
    }

    public static int GenerateOffset()
    {
        return Random.Range(0, 5);
    }

    void SetOffset()
    {
        offset = Game.GET_GAME_STATE().GetGameFlags().GetOffset();
    }

    public int GetCharacterAddress(int toIndex)
    {
        int characterIndex = (offset + toIndex) % CharacterGenerator.getRoleNames().Length;
        int address = GetAllMailBoxAddresses()[characterIndex];
        return address;
    }

    public static int[] GenerateCharacterAddresses(int mailboxCount)
    {
        if (AllAddresses.Count != 0) AllAddresses.Clear();
        for (int i = 0; i < mailboxCount * 2; i++) // Assuming each mailbox has 2 addresses
        {
            int address;
            // Generate a random unique address between 10000 and 99999
            do
            {
                address = GenerateCharacterAddress();
            } while (AllAddresses.Contains(address));
            AllAddresses.Add(address);
        }
        return AllAddresses.ToArray();
    }

    public void FindAllMailBoxes()
    {
        GameObject[] boxes = GameObject.FindGameObjectsWithTag("Mail Container");
        mailboxes.Clear();
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
        if (mailboxes == null || mailboxes.Count == 0) FindAllMailBoxes();
        if (AllAddresses.Count == 0)
            GenerateCharacterAddresses(mailboxes.Count);
        return AllAddresses.ToArray();
    }

    public void SetMailBoxAddresses(int[] _addresses)
    {
        if (mailboxes == null || mailboxes.Count == 0)
        {
            Debug.LogError("No mailboxes found in the scene.");
        }
        else
        {
            int index = 0;
            AllAddresses.Clear();
            foreach (Mailbox mailbox in mailboxes)
            {
                mailbox.ClearAddresses();
                if (index < _addresses.Length)
                {
                    mailbox.addAddress(_addresses[index]);
                    AllAddresses.Add(_addresses[index]);
                    index++;
                    mailbox.addAddress(_addresses[index]);
                    AllAddresses.Add(_addresses[index]);
                    index++;
                }
            }
        }
    }

    public void PopulateMailboxes(int dayIndex)
    {
        List<Letter> letters = GenerateMail(dayIndex);
        foreach (Letter letter in letters)
        {
            Mailbox mailbox = GetMailbox(letter.toIndex);
            if (mailbox != null)
            {
                mailbox.GenerateMail(letter);
            }
        }
    }
    
    private Mailbox GetMailbox(int index)
    {
        string name = string.Format("Mailbox {0}", CalculateMailbox(index));
        FindAllMailBoxes();
        return mailboxes.Find(x =>
        {
            if (x != null)
                return x.name == name;
            else
                return false;
        });
    }

    private List<Letter> GenerateMail(int dayIndex)
    {
        List<Letter> letters = new List<Letter>(), allLetters = FileManager.GetLetters();
        List<Probability> letterChances = FileManager.LoadDayTable().days[dayIndex].probabilities.letterProbabilities;
        foreach (Probability chance in letterChances)
        {
            Letter letter = allLetters.Find(x => x.GetUID() == chance.UID);
            if (letter != null && CheckRequirements(letter))
            {
                int roll = ChanceRoll();
                Debug.Log(string.Format("Chance probability: {0}.\n Rolled {1}.\n Delivered? {2}", chance.probability * 100, roll, Deliver(roll, chance.probability) ? "Yes" : "No"));
                if (Deliver(roll, chance.probability)) letters.Add(letter);
            }
        }
        return letters;
    }
    
    private bool Deliver(int roll, double probability)
    {
        return roll >= probability * 100;
    }

    private int ChanceRoll()
    {
        return Random.Range(0, 101);
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
        return (offset + mailBoxIndex) % mailboxes.Count;
    }

    private static int GenerateCharacterAddress()
    {
        return Random.Range(10000, 99999);
    }
    
    struct LetterProbabilityTable {
        string id;
        float probability;
    }

}
