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
    public static Sprite[] sprite;
    // Start is called before the first frame update
    public static void generateCharacters(GameState gameState)
    {
        // bool loaded = false;
        // if (gameState == null || gameState.characters == null)
        // {
        //     Debug.LogError("GameState component not found on GameObject with tag 'GameState'.");
        //     return;
        // }

        for (int i = 0; i < characterNames.Length; i++)
        {
            Character character = new Character();
            character.setName(characterNames[i]);
            character.setAddress(generateRandomCharacterAddress());
            // character.setSymbol(sprite[i]);
            // Debug.Log("Symbol for " + character.getName() + ": " + character.getSymbol());
            gameState.addCharacter(character);
        }
        // FileManager.SaveCharacters(gameState.characters);
        
    }

    private static void getSprites()
    {
        for (int i = 0; i < sprite.Length; i++)
        {
            sprite[i] = Resources.Load<Sprite>("Sprites/Character" + (i + 1)); // Assuming your sprites are named "Character1", "Character2", etc. and are located in a Resources folder
            if (sprite[i] == null)
            {
                Debug.LogError("Sprite not found for Character" + (i + 1));
            }
        }
    }

    static public string[] getCharacterNames()
    {
        return characterNames;
    }

    static int generateRandomCharacterAddress()
    {
        return Random.Range(1000, 9999);
    }

}
