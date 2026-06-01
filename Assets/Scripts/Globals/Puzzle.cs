public abstract class Puzzle
{
    public string puzzleText = "";
    public abstract Puzzle CreatePuzzle(Letter letter);
    public abstract void EndPuzzle();
}
