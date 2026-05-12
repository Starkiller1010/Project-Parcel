using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

public class FileManager
{
    public static string Directory = FileUtils.GetSaveDirectory();
    public static string SAVE_DIRECTORY = "/SaveGames";

    public static void SaveGameState(GameState gameState)
    {
        SaveState meta = new SaveState()
        {
            created_at = DateTime.UtcNow.ToString("o"),
            updated_at = DateTime.UtcNow.ToString("o"),
            playtime = Game.GET_TIME_TRACKER().GetTimer().GetPlayTime(),
            dayCount = Game.GET_TIME_TRACKER().GetDay(),
            characterAddresses = gameState.GetMailSystem().GetAllMailBoxAddresses(),
            offset = gameState.GetGameFlags().GetOffset(),
            flags = FileUtils.StringifyFlags(gameState.GetGameFlags().GetMarkers())
        };
        string json = JsonUtility.ToJson(meta);
        FileUtils.WriteFile(FileUtils.CreateSaveFileName(DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")), json);
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
        FileUtils.ReadFile(fileName);
        SaveState loadState = JsonUtility.FromJson<SaveState>(json);
        return FileUtils.ParseSaveStateIntoGameState(loadState);
    }

    public static TextAsset[] GetLetterFiles()
    {
        // string path = "Letter/Day " + Game.GET_TIME_TRACKER().GetDay() + "/Default.txt";
        string path = "Letter/Day 0";
        Debug.Log("Attempting to load letter files from path: " + path);
        TextAsset[] letterFiles = Resources.LoadAll<TextAsset>(path);
        if (letterFiles == null || letterFiles.Length == 0)
        {
            Debug.LogError("No letter files found at path: " + path);
            return null;
        }
        foreach (TextAsset letterFile in letterFiles)
        {
            Debug.Log("Loaded letter file: " + letterFile.name);
        }
        return letterFiles;
    }
}