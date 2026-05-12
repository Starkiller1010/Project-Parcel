using System;
using System.Collections.Generic;

[Serializable]
public class DayTable
{
    public List<Day> day { get; set; }
}

public class Day
{
    public object dialogues { get; set; }
    public Probabilities probabilities { get; set; }
}

public class LetterProbability
{
    public int UID { get; set; }
    public double Probability { get; set; }
}

public class Probabilities
{
    public List<LetterProbability> letters { get; set; }
}

