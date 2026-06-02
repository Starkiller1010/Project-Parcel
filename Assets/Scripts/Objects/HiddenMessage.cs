using System.Linq;
using UnityEngine;

public class HiddenMessage : Puzzle
{
    char[] hiddenMessage = "DoctorDidIt".ToCharArray();
    public override Puzzle CreatePuzzle(Letter letter)
    {
        string text = FileManager.GetLetterContent(letter);
        HideMessage(text);
        return this;
    }

    public override PuzzleName GetPuzzleName()
    {
        return PuzzleName.HiddenMessage;
    }

    private void HideMessage(string text)
    {
        int index = 0;
        if (text.Length > hiddenMessage.Length)
        {
            text.Split(' ').ToList().ForEach(x =>
            {
                if (x.Length > 3 && index < hiddenMessage.Length)
                {
                    puzzleText += SplitWordWithChar(x, hiddenMessage[index]) + " ";
                    index++;
                }
                else
                {
                    puzzleText += x + " ";
                }
            });
        }
        // else
        // {
        //     puzzleText = string.Join(" ", hiddenMessage);
        // }
    }

    private string SplitWordWithChar(string word, char splitChar)
    {
        int splitIndex = Random.Range(1, word.Length - 2);
        return word.Substring(0, splitIndex) + splitChar + word.Substring(splitIndex);
    }
    
    private char GetCharAtIndex(string text, int index)
    {
        if (index < text.Length)
        {
            return text[index];
        }
        return ' ';
    }
}