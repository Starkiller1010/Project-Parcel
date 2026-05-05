using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class FileManager
{
    public static string saveDirectory = Application.persistentDataPath + "/SaveGames";
    public static string debugDirectory = Application.dataPath;
    private static string fileName = "SaveGame_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".json";
    private static string debugFilePath = debugDirectory + "/" + fileName;
    private static string saveFilePath = saveDirectory + "/" + fileName;

    public static void SaveGameState(GameState gameState)
    {
        if (!Directory.Exists(saveDirectory))
        {
            Directory.CreateDirectory(saveDirectory);
        }
        SaveState meta = new SaveState()
        {
            created_at = DateTime.UtcNow.ToString("o"),
            updated_at = DateTime.UtcNow.ToString("o"),
            playtime = gameState.GetTimeTracker().GetPlayTime(),
            dayCount = gameState.GetTimeTracker().GetDay(),
            characterAddresses = gameState.GetMailSystem().GetAllMailBoxAddresses(),
            offset = gameState.GetGameFlags().GetOffset(),
            flags = SerializeFlags(gameState.GetGameFlags().GetMarkers())
        };
        string json = JsonUtility.ToJson(meta);
#if UNITY_EDITOR
        System.IO.File.WriteAllText(debugFilePath, json);
        Debug.Log("Game state saved to: " + debugFilePath);
#else
        System.IO.File.WriteAllText(saveFilePath, json);
        Debug.Log("Game state saved to: " + saveFilePath);
#endif
    }

    public static string[] GetSavedGameStates(string directory)
    {
        string[] files = System.IO.Directory.GetFiles(directory, "SaveGame_*.json");
        for (int i = 0; i < files.Length; i++)
        {
            files[i] = System.IO.Path.GetFileNameWithoutExtension(files[i]);
        }
        return files;
    }

    public static GameState LoadGameState(string fileName)
    {
        string json = null;
#if UNITY_EDITOR
        json = System.IO.File.ReadAllText(debugDirectory + "/" + fileName + ".json");
        Debug.Log("Game state loaded from: " + debugDirectory + "/" + fileName + ".json");
#else
        json = System.IO.File.ReadAllText(saveDirectory + "/" + fileName + ".json");
        Debug.Log("Game state loaded from: " + saveFilePath);
#endif
        SaveState loadState = JsonUtility.FromJson<SaveState>(json);
        return ConvertSaveStateToGameState(loadState);
    }

    // public static TextAsset LoadLetterFile(string fileName)
    // {
    //     return LoadTextFile("Letter", fileName);
    // }

    // public static TextAsset LoadDialogueFile(string fileName)
    // {
    //     return LoadTextFile("Dialogue", fileName);
    // }

    public static TextAsset LoadTextFile(string directory, string fileName)
    {
        // Path within the Resources folder, without file extension
        string fullPath = directory + "/" + fileName;
        return Resources.Load<TextAsset>(fullPath);
    }

    private static GameState ConvertSaveStateToGameState(SaveState saveState)
    {
        GameState game = new GameState(
            dayCount: saveState.dayCount,
            offset: saveState.offset,
            addresses: saveState.characterAddresses,
            flags: DeserializeFlags(saveState.flags),
            playTime: saveState.playtime);
        return game;
    }

    // public static void StateInfo(GameState state)
    // {
    //     if (state == null) Debug.LogError("Game State is null");
    //     if (state.GetTimer() == null) Debug.LogError("Timer is null");
    //     if (state.GetCurrentDay() == null) Debug.LogError("Day is null");
    //     if (state.addresses == null || state.addresses.Length == 0) Debug.LogError("Addresses are null or empty");
    //     Debug.Log(state.offset);

    //     // Debug.Log("Conversion complete. " +
    //     //     "\nPlaytime: " + state.GetTimer().GetPlayTime() +
    //     //     ",\nDay Count: " + state.GetCurrentDay() +
    //     //     ",\nOffset: " + state.offset +
    //     //     ",\nAddresses: " + string.Join(", ", state.GetAllAddresses()));
    // }

    public static string SerializeFlags(bool[,] flagGrid)
    {
        string flags = "";
        int characterCount = flagGrid.GetLength(0);
        int flagCount = flagGrid.GetLength(1);
        for (int row = 0; row < characterCount; row++)
        {
            flags += "{";
            for (int col = 0; col < flagCount; col++)
            {
                flags += flagGrid[row, col].ToString();
                if (col + 1 != flagCount) flags += ",";
            }
            flags += "}";
        }
        Debug.Log("Serialized Flags: " + flags);
        return flags;
    }

    public static bool[,] DeserializeFlags(string json)
    {
        bool[,] flagGrid = new bool[8, 5];
        string[] parts = json.Split('{', '}', ',');
        int characterCount = flagGrid.GetLength(0);
        int flagCount = flagGrid.GetLength(1);
        int index = 0;
        List<bool> flags = new List<bool>();
        foreach (string part in parts)
        {
            if (part != "" && part != null)
            {
                flags.Add(bool.Parse(part));
            }
        }
        for (int row = 0; row < characterCount; row++)
        {
            for (int col = 0; col < flagCount; col++)
            {
                flagGrid[row, col] = flags[index];
                index++;
            }
        }
        return flagGrid;
    }

    [Serializable]
    class SaveState
    {
        public string version;
        public string created_at;
        public string updated_at;
        public string playtime;
        public int dayCount;
        public int offset;
        public int[] characterAddresses;
        public string flags;

        public SaveState(string version = "1.0",
                        string created_at = "",
                        string updated_at = "",
                        string playtime = "0:00:00",
                        int dayCount = 0,
                        int offset = 0,
                        int[] characterAddresses = null,
                        string flags = null)
        {
            this.version = version;
            this.created_at = created_at;
            this.updated_at = updated_at;
            this.playtime = playtime;
            this.dayCount = dayCount;
            this.offset = offset;
            this.characterAddresses = characterAddresses;
            this.flags = flags;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("version: " + this.version + "\n");
            stringBuilder.Append("created_at: " + this.created_at + "\n");
            stringBuilder.Append("updated_at: " + this.updated_at + "\n");
            stringBuilder.Append("playtime: " + this.playtime + "\n");
            stringBuilder.Append("dayCount: " + this.dayCount + "\n");
            stringBuilder.Append("offset: " + this.offset + "\n");
            stringBuilder.Append("flags: " + this.flags + "\n");
            return stringBuilder.ToString();
        }
    }
}