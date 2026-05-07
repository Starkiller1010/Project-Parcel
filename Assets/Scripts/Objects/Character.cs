using UnityEngine;

public class Character
{
    private int Address;
    private string Name;
    private Sprite Symbol;

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

    internal void setSymbol(Sprite v)
    {
        this.Symbol = v;
    }

    internal Sprite getSymbol()
    {
        return this.Symbol;
    }
}
