using UnityEngine;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System;

static class FileUtils
{
    // static List<string> SavedDirectories = new List<string>();
    public static string SAVE_DIRECTORY = "/SaveGames";

    public static string GetRootDirectory()
    {
        return Application.dataPath;
    }

    public static string GetSaveDirectory()
    {
        string path = "";
#if UNITY_EDITOR
        path = Application.dataPath;
#else
        path = Application.persistentDataPath;
#endif
        return path;
    }

    public static string GetResourcesDirectory(string file = "")
    {
        return GetRootDirectory() + "/Resources" + file;
    }

    // public static void MakeDirectories(string[] directories)
    // {
    //     foreach (string directory in directories)
    //     {
    //         string path = GetRootDirectory() + "/" + directory;
    //         if (!Directory.Exists(path))
    //         {
    //             Directory.CreateDirectory(path);
    //             SavedDirectories.Add(directory);
    //         }
    //     }
    // }

    public static void WriteJsonFile<T>(T state, string fileName)
    {
        string json = JsonConvert.SerializeObject(state);
        WriteFile(fileName, json);
    }

    public static GameState ParseSaveStateIntoGameState(SaveState saveState)
    {
        GameState game = new GameState(
            dayCount: saveState.dayCount,
            offset: saveState.offset,
            addresses: saveState.characterAddresses,
            // flags: ParseFlags(saveState.flags),
            playTime: saveState.playtime,
            characterNames: saveState.characterNames);
        return game;
    }

    public static string[] GetFiles(string filePath, string regex)
    {
        string[] files = System.IO.Directory.GetFiles(GetRootDirectory() + filePath, regex);
        for (int i = 0; i < files.Length; i++)
        {
            files[i] = System.IO.Path.GetFileNameWithoutExtension(files[i]);
        }
        return files;
    }

    private static string ReadFile(string filePath)
    {
        string file = System.IO.File.ReadAllText(filePath);
        if (file == null)
        {
            Debug.LogError(string.Format("Failed to read file at {0}", filePath));
        }
        return file;
    }

    private static void WriteFile(string filePath, string content)
    {
        System.IO.File.WriteAllText(GetRootDirectory() + filePath, content);
    }

    public static TextAsset LoadTextFile(string filePath)
    {
        return Resources.Load<TextAsset>(filePath);
    }

    public static T LoadJsonFile<T>(string filePath)
    {
        T file = JsonConvert.DeserializeObject<T>(ReadFile(filePath + ".json"));
        return file;
    }


    public static string StringifyFlags(bool[,] flagGrid)
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

    public static bool[,] ParseFlags(string json)
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
}