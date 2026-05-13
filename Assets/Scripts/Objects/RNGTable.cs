// DayTable myDeserializedClass = JsonConvert.DeserializeObject<DayTable>(myJsonResponse);
using System;
using System.Collections.Generic;
using System.Text;

[Serializable]
public class RNGTable
{
    public List<Day> days { get; set; }

    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();
        if (days == null || days.Count == 0)
        {
            builder.Append("Days are null or empty");
        }
        else
        {
            int index = 0;
            foreach (Day _day in days)
            {
                builder.Append(string.Format("Day {0}\n", index));
                if (_day.probabilities != null)
                {
                    Probabilities _prop = _day.probabilities;
                    if (_prop != null)
                    {
                        builder.Append("Letter UIDs Found: ");
                        foreach (Probability letter in _prop.letterProbabilities)
                            builder.Append(letter.UID + ":");

                        builder.Append("\nEvent UIDs Found: ");
                        foreach(Probability _event in _prop.eventProbabilities) 
                            builder.Append(_event.UID + ":");
                    }
                }
                else
                {
                    builder.Append("Probabilities is null");
                }
                index++;
                builder.Append("\n");
            }
        }
        return builder.ToString();
    }
}

public class Day
{
    public Probabilities probabilities { get; set; }
}

public class Probability
{
    public int UID { get; set; }
    public double probability { get; set; }
}

public class Probabilities
{
    public List<Probability> letterProbabilities { get; set; }
    public List<Probability> eventProbabilities { get; set; }
}
