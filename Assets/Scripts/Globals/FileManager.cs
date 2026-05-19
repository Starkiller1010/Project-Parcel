using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class FileManager
{
    private static string RNG_TABLE_FILE_NAME = "/RNGTable";
    private static string LETTERS_FILE_NAME = "/Letters";
    private static string NAMES_FILE_NAME = "/Names";

    public static void SaveGameState(GameState gameState)
    {
        SaveState meta = new SaveState()
        {
            created_at = DateTime.UtcNow.ToString("o"),
            updated_at = DateTime.UtcNow.ToString("o"),
            playtime = Game.GET_TIME_TRACKER().GetTimer().GetPlayTime(),
            dayCount = Game.GET_TIME_TRACKER().GetDay(),
            characterAddresses = gameState.GetMailSystem().GetAllMailBoxAddresses(),
            characterNames = gameState.GetCharacterNames(),
            offset = gameState.GetGameFlags().GetOffset()
            // flags = FileUtils.StringifyFlags(gameState.GetGameFlags().GetMarkers())
        };
        FileUtils.SaveGameFile(meta);
    }

    public static string[] GetSavedGameStates()
    {
        return FileUtils.GetFiles(FileUtils.SAVE_DIRECTORY, "SaveGame_*.json");
    }

    public static RNGTable LoadDayTable()
    {
        string filePath = FileUtils.GetResourcesDirectory(RNG_TABLE_FILE_NAME);
        RNGTable table = FileUtils.LoadJsonFile<RNGTable>(filePath);
        return table;
    }

    public static Names LoadNames()
    {
        string filePath = FileUtils.GetResourcesDirectory(NAMES_FILE_NAME);
        Names names = FileUtils.LoadJsonFile<Names>(filePath);
        return names;
    }


    public static GameState LoadGameState(string fileName)
    {
        SaveState loadState = FileUtils.LoadJsonFile<SaveState>(fileName);
        return FileUtils.ParseSaveStateIntoGameState(loadState);
    }

    public static List<Letter> GetLetters()
    {
        string filePath = FileUtils.GetResourcesDirectory(LETTERS_FILE_NAME);
        Letters letters = FileUtils.LoadJsonFile<Letters>(filePath);
        return letters.letters;
    }

    public static TextAsset[] GetLetterFiles(int dayIndex)
    {
        string path = "Letter/Day " + dayIndex;
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