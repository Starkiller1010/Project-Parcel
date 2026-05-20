using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterGenerator
{
    private static string[] roleNames = new string[] {
        "Diplomat",
        "Wife",
        "Mistress",
        "Blackmailer",
        "Doctor",
        "Mother",
        "Cult Leader",
        "Kidnapper" };
    // Start is called before the first frame update
    public static List<Character> generateCharacters(int[] addresses)
    {
        List<Character> characters = new List<Character>();
        foreach (int address in addresses)
        {
            characters.Add(generateCharacter(address, GetCharacterNames()));
        }
        return characters;
    }

    public static List<Character> generateCharacters(int[] addresses, string[] names)
    {
        List<Character> characters = new List<Character>();
        int index = 0;
        foreach (int address in addresses)
        {
            characters.Add(generateCharacter(name: names[index], address: address));
            index++;
        }
        return characters;
    }

    private static Character generateCharacter(int address, Names names)
    {
        string firstName = getRandomName(names.first);
        names.first.Remove(firstName);
        string lastName = getRandomName(names.last);
        string name = string.Join(" ", firstName, lastName);
        Character character = new Character(name, address);
        return character;
    }

    private static Character generateCharacter(int address, string name)
    {
        Character character = new Character(name, address);
        return character;
    }

    private static string getRandomName(List<string> names)
    {
        return names[Random.Range(0, names.Count)];
    }

    static public string[] getRoleNames()
    {
        return roleNames;
    }

    static public Names GetCharacterNames()
    {
        return FileManager.LoadNames();
    }

}
