
using System;
using System.Collections.Generic;

[Serializable]
public class DayTable
{
    Day[] days;


    struct Day
    {
        string id;

    }

    struct Dialogues
    {
        string id;
        string content;
    }


    struct Probabilities
    {
        LetterProbabilities[] letterProbabilities;
    }

    struct LetterProbabilities
    {
        string id;
        float probability;
    }
}