using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

[Serializable]
public class Character
{

    [JsonProperty]
    private string Name;
    [JsonProperty]
    private int Address;
    [JsonProperty(NullValueHandling = NullValueHandling.Include)]
    private List<int?> ReceivedLetters = new List<int?>();

    public Character(string name, int address)
    {
        setName(name);
        setAddress(address);
    }

    public void setName(string v)
    {
        this.Name = v;
    }

    public string getName()
    {
        return this.Name;
    }

    public void setAddress(int v)
    {
        this.Address = v;
    }

    public int getAddress()
    {
        return this.Address;
    }

    public List<int?> getLetters()
    {
        return ReceivedLetters;
    }

    public void setLetters(List<int?> letters)
    {
        ReceivedLetters = letters;
    }

    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("Name: " + getName());
        builder.Append("\nAddress: " + getAddress());
        builder.Append("\nReceived Letters:" + getLetters());
        return builder.ToString();
    }
}
