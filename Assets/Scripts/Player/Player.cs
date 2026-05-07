using System.Collections.Generic;

public class Player
{
    static List<Letter> collectedLetter = new List<Letter>();

    static Controls controls = null;

    public static void AddLetter(Letter letter)
    {
        collectedLetter.Add(letter);
    }

    public static List<Letter> GetCollectedLetters()
    {
        return collectedLetter;
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
