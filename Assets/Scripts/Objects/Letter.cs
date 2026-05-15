using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Newtonsoft.Json;
using Unity.VisualScripting;

public class Letters
{
    public List<Letter> letters { get; set; }

    public override string ToString()
    {
        List<string> output = new List<string>();
        foreach(Letter letter in letters)
        {
            output.Add(letter.ToString() + "\n");
        }
        return string.Join(", ", output);
    }
}

[Serializable]
public class Letter
{
    public int UID;
    public int fromIndex;
    public int toIndex;
    public string content;
    public List<int> requirements;

    public int GetUID()
    {
        return UID;
    }

    public void SetUID(int newUid)
    {
        UID = newUid;
    }

    public int GetToIndex()
    {
        return toIndex;
    }

    public void SetToIndex(int newAddress)
    {
        toIndex = newAddress;
    }

    public string GetContent()
    {
        return content;
    }

    public void SetContent(string content)
    {
        this.content = content;
    }

    public int GetFromIndex()
    {
        return fromIndex;
    }

    public void SetFromIndex(int newIndex)
    {
        fromIndex = newIndex;
    }

    public List<int> GetRequirements()
    {
        return requirements;
    }

    public void SetRequirements(int[] requirements)
    {
        if (requirements != null)
        {
            this.requirements = new List<int>(requirements);
        }
    }

    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();
        string template = "{0}: {1}\n";
        builder.Append(string.Format(template, "UID", GetUID()));
        builder.Append(string.Format(template, "To", GetToIndex()));
        builder.Append(string.Format(template, "From", GetFromIndex()));
        builder.Append(string.Format(template, "Requirements", GetRequirements().ToSeparatedString(",")));
        builder.Append(string.Format(template, "Content", GetContent()));
        return builder.ToString();
    }
}
