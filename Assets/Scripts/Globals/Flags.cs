using System;
using Random = UnityEngine.Random;
using System.Collections.Generic;

public class Flags
{
    // private List<Letter> completedLetters = new List<Letter>();
    private Dictionary<int, Letter> completedLetters = new Dictionary<int, Letter>();
    private int mailboxOffset = -1;
    private bool[,] markers = null;

    public int GetOffset()
    {
        if (mailboxOffset == -1)
        {
            mailboxOffset = Random.Range(0, 5);
        }
        return mailboxOffset;
    }

    public void SetOffset(int offset)
    {
        this.mailboxOffset = offset;
    }

    public Dictionary<int, Letter> GetCompletedLetters()
    {
        return completedLetters;
    }

    public void AddCompletedLetter(Letter letter)
    {
        completedLetters.Add(letter.UID, letter);
        Player.GetLetters().Remove(letter);
    }

    public bool[,] GetMarkers()
    {
        return markers;
    }

    public void SetFlags(bool[,] flags)
    {
        this.markers = flags;
    }
}