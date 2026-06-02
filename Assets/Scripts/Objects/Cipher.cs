using System.Linq;
using Random = UnityEngine.Random;

public class Cipher : Puzzle
{
    public override Puzzle CreatePuzzle(Letter letter)
    {
        string text = FileManager.GetLetterContent(letter);
        CreateCipher(text);
        return this;
    }

    public override PuzzleName GetPuzzleName()
    {
        return PuzzleName.Cipher;
    }

    private void CreateCipher(string text)
    {
        int shift = Random.Range(3, 26); // Shift value for a Caesar cipher
        text.Split(' ', '\n').ToList().ForEach(x =>
        {
            char cipherChar;
            foreach (char c in x)
            {
                if (!char.IsLetter(c))
                {
                    puzzleText += c; // Non-letter characters are added unchanged
                    puzzleText += "\n"; // Add a newline after non-letter characters to separate words
                    continue;
                }
                if (char.IsUpper(c))
                {
                    // Example cipher logic: Shift each character by 3 positions in the alphabet
                    cipherChar = (char)(((c - 'A' + shift) % 26) + 'A');
                    puzzleText += cipherChar;
                    continue;
                }

                // Example cipher logic: Shift each character by 3 positions in the alphabet
                cipherChar = (char)(((c - 'a' + shift) % 26) + 'a');
                puzzleText += cipherChar;
            }
            puzzleText += " "; // Add space between words
        });
    }
}