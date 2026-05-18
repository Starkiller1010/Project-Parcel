using System.Text;
using UnityEngine;

public class Character
{
    private int Address;
    private string Name;

    internal void setName(string v)
    {
        this.Name = v;
    }

    internal string getName()
    {
        return this.Name;
    }

    internal void setAddress(int v)
    {
        this.Address = v;
    }

    internal int getAddress()
    {
        return this.Address;
    }

    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("Name: " + getName());
        builder.Append("\nAddress: " + getAddress());
        return builder.ToString();
    }
}
