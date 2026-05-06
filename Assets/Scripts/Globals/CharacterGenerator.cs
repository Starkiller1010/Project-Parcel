using UnityEngine;

public class CharacterGenerator
{
    private static string[] characterNames = new string[] {
        "Diplomat",
        "Wife",
        "Mistress",
        "Blackmailer",
        "Doctor",
        "Mother",
        "Cult Leader",
        "Kidnapper" };
    // Start is called before the first frame update
    public static void generateCharacters(int[] addresses, int offset = 0)
    {
        int index = offset * 2; // Start index for character names based on the offset, multiplied by 2 because each mailbox has 2 addresses
        foreach (int address in addresses)
        {
            generateCharacter(address, index);
            index = (index + 1) % characterNames.Length; // Move to the next character for the next address, wrap around if we exceed the array length
        }
    }

    public static void generateCharacter(int address, int offset)
    {
        Character character = new Character();
        character.setName(characterNames[offset]);
        character.setAddress(address);
    }

    static public string[] getCharacterNames()
    {
        return characterNames;
    }



}
