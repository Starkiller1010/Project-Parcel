using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class GameState2 : MonoBehaviour
{
    TimeTracker timeTracker = new TimeTracker();
    List<Mailbox> mailboxes = new List<Mailbox>();
    Flags gameFlags = new Flags();

    public void Start()
    {
        InitMailBoxes();
    }

    public void InitMailBoxes()
    {
        GameObject[] boxes = GameObject.FindGameObjectsWithTag("MailBox");
        foreach (GameObject box in boxes) mailboxes.Add(box.GetComponent<Mailbox>());
    }

    public GameState2(int dayCount, int[] addresses, int offset, string playTime, bool[,] flags = null)
    {
        timeTracker.SetDay(dayCount);
        timeTracker.GetTimer().SetTimer(playTime);
        SetAddresses(addresses, offset);
        gameFlags.markers = flags;
    }

    public void SetState(int dayCount, int[] addresses, int offset, bool[,] flags, string timer)
    {
        
    }

    public void SetState(GameState _state)
    {
        
    }
    
    public void SetAddresses(int[] character_addresses, int offset)
    {
        
    }
}
