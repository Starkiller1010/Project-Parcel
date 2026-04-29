using Unity.VisualScripting;
using UnityEngine;

public class Letter
{
    private int uuid;
    private Sprite fromSymbol;
    private int toAddress;
    private string content;
    public GameObject gameObject; // Reference to the GameObject representing the letter in the scene

    public Letter(Sprite fromSymbol, int toAddress, string content, int uuid = 0)
    {
        this.uuid = uuid;
        this.fromSymbol = fromSymbol;
        this.toAddress = toAddress;
        this.content = content;
    }

    public int getAddress()
    {
        return toAddress;
    }

    public int setAddress(int newAddress)
    {
        toAddress = newAddress;
        return toAddress;
    }   

    public string getContent()
    {
        return content;
    }

    public string getFromSymbol()
    {
        return fromSymbol.name; // Return the name of the sprite as the symbol representing the sender
    }

    public void setFromSymbol(Sprite newSymbol)
    {
        fromSymbol = newSymbol;
        this.gameObject = new GameObject("Letter for " + toAddress); // Create a new GameObject for the letter
        SpriteRenderer renderer = this.gameObject.AddComponent<SpriteRenderer>(); // Add a SpriteRenderer component to the GameObject
        renderer.sprite = fromSymbol; // Set the sprite of the SpriteRenderer to the symbol representing the sender
    }
}
