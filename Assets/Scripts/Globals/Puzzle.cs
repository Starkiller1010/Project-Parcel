public abstract class Puzzle
{
    public enum PuzzleName
    {
        HiddenMessage,
        Cipher,
        Mirror
    }
    protected string puzzleText = "";
    public abstract Puzzle CreatePuzzle(Letter letter);
    public abstract PuzzleName GetPuzzleName();
    public virtual string GetPuzzleText()
    {
        return puzzleText;
    }
}

public static class PuzzleFactory
{
    public static Puzzle CreatePuzzle(Puzzle.PuzzleName puzzleName, Letter letter)
    {
        switch (puzzleName)
        {
            // case Puzzle.PuzzleName.HiddenMessage:
            //     return new HiddenMessage().CreatePuzzle(letter);
            case Puzzle.PuzzleName.Cipher:
                return new Cipher().CreatePuzzle(letter);
            case Puzzle.PuzzleName.Mirror:
                return new Mirror().CreatePuzzle(letter);
            default:
                throw new System.ArgumentException("Invalid puzzle name");
        }
    }
}
