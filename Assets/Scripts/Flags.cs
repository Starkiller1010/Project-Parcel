using System.Collections.Generic;

public class Flags
{
    public List<Letter> completedLetters = new List<Letter>();
    public int mailboxOffset = -1;
    public bool[,] markers = null;

    public void SetFlags(bool[,] flags)
    {
        this.markers = flags;
    }
}