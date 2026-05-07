using System.Collections;
using UnityEngine;
using System.Collections.Generic;


public class MailSystem
{
    private List<Mailbox> mailboxes = new List<Mailbox>();

    public MailSystem()
    {
        FindAllMailBoxes();
    }

    public MailSystem(int[] character_addresses)
    {
        FindAllMailBoxes();
        SetMailBoxAddresses(character_addresses);
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
    
    private static int GenerateCharacterAddress()
    {
        return Random.Range(1000, 9999);
    }

}
