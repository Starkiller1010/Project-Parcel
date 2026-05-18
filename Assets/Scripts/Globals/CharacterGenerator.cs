using System.Collections.Generic;
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

    private static string[] characterNames = null;
    // Start is called before the first frame update
    public static List<Character> generateCharacters(int[] addresses, int offset = 0)
    {
        characterNames = new string[roleNames.Length];
        List<Character> characters = new List<Character>();
        Names names = FileManager.LoadNames();
        foreach (int address in addresses)
        {
            characters.Add(generateCharacter(address, names));
        }
        return characters;
    }

    public static Character generateCharacter(int address, Names names)
    {
        Character character = new Character();
        string firstName = getRandomName(names.first);
        names.first.Remove(firstName);
        string lastName = getRandomName(names.last);
        string name = string.Join(" ", firstName, lastName);
        character.setName(name);
        character.setAddress(address);
        Debug.Log(character);
        return character;
    }

    private static string getRandomName(List<string> names)
    {
        return names[Random.Range(0, names.Count)];
    }

    static public string[] getCharacterNames()
    {
        return characterNames;
    }

    static public string[] getRoleNames()
    {
        return roleNames;
    }

}
