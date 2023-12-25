using System.IO;
using UnityEngine;

public static class SaveManager
{
    public static string _directory = "/SaveData/";
    public static string _fileName = "Settings.txt";

    public static void SaveSettings(SaveSettings settings)
    {
        string dir = Application.persistentDataPath + _directory;
        if (!Directory.Exists(dir)) 
        {
            Directory.CreateDirectory(dir);
        }

        string json = JsonUtility.ToJson(settings);
        File.WriteAllText(dir + _fileName, json);
    }
    public static SaveSettings Load()
    {
        string fullPath = Application.persistentDataPath + _directory + _fileName;
        SaveSettings newSettings = new SaveSettings();

        if(File.Exists(fullPath))
        {
            string json = File.ReadAllText(fullPath);
            newSettings = JsonUtility.FromJson<SaveSettings>(json);
        }
        return newSettings;
    }
}
