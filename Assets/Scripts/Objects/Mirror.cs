using System.Linq;
using UnityEngine;

public class Mirror : Puzzle
{
    public override Puzzle CreatePuzzle(Letter letter)
    {
        string text = FileManager.GetLetterContent(letter);
        puzzleText = string.Join(" ", text.Split(' ').Select(word => new string(word.Reverse().ToArray())));
        return this;
    }

    public override PuzzleName GetPuzzleName()
    {
        return PuzzleName.Mirror;
    }
}