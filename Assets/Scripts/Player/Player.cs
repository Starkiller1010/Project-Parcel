using System.Collections.Generic;

public class Player
{
    static List<Letter> collectedLetter = new List<Letter>();

    static Controls controls = null;

    public static void AddLetter(Letter letter)
    {
        collectedLetter.Add(letter);
    }

    public static List<Letter> GetLetters()
    {
        return collectedLetter;
    }

    public static List<Letter> ClearCollectedLetters()
    {
        List<Letter> lettersToReturn = new List<Letter>(collectedLetter);
        collectedLetter.Clear();
        return lettersToReturn;
    }

    public static void AddLetters(List<Letter> letters)
    {
        collectedLetter.AddRange(letters);
    }

    public static void AddLetters(Letter[] letters)
    {
        collectedLetter.AddRange(letters);
    }

    public Player()
    {
        GetControls();
    }

    public Controls GetControls()
    {
        if (controls == null)
        {
            controls = new Controls();
        }
        return controls;
    }

}
