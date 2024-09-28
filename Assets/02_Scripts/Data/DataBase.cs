using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;


public class DataBase
{
    public static AllData data;
    public static Dictionary<string, Sprite> sprites;

    static Sprite LoadSprite(string id)
    {
        var filePath = Application.streamingAssetsPath + "/data/images/carnet/" + id + ".png";
        if (!File.Exists(filePath)) {
            Debug.LogError("Can't the image for object which id is " + id + ". The expected file shloud is " + filePath + ". Use a default one to avoid a crash.");
            filePath = Application.streamingAssetsPath + "/data/images/carnet/not_available.png";
        }
        // Converts desired path into byte array
        byte[] pngBytes = System.IO.File.ReadAllBytes(filePath);

        // Creates texture and loads byte array data to create image
        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(pngBytes);

        // Creates a new Sprite based on the Texture2D
        Sprite sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);

        return sprite;
    }

    public static void LoadData()
    {
        data = SaveLoad<AllData>.Load("data", "data.json");

        // Load and cache all sprites for object images.
        sprites = new Dictionary<string, Sprite>();
        foreach (var obj in data.objects)
        {
            Sprite sprite = LoadSprite(obj.ID);
            sprites.Add(obj.ID, sprite);
        }
    }
}

static class SaveLoad<T>
{
    /// <summary>
    /// Save data to a file (overwrite completely)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <param name="folder"></param>
    /// <param name="file"></param>
    public static void Save(T data, string folder, string file)
    {
        // get the data path of this save data
        string dataPath = GetFilePath(folder, file);

        string jsonData = JsonUtility.ToJson(data, true);
        byte[] byteData;

        byteData = Encoding.ASCII.GetBytes(jsonData);

        // create the file in the path if it doesn't exist
        // if the file path or name does not exist, return the default SO
        if (!Directory.Exists(Path.GetDirectoryName(dataPath)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(dataPath));
        }

        // attempt to save here data
        try
        {
            // save datahere
            File.WriteAllBytes(dataPath, byteData);
            Debug.Log("Save data to: " + dataPath);
        }
        catch (Exception e)
        {
            // write out error here
            Debug.LogError("Failed to save data to: " + dataPath);
            Debug.LogError("Error " + e.Message);
        }
    }

    /// <summary>
    /// Load all data at a specified file and folder location
    /// </summary>
    /// <param name="folder"></param>
    /// <param name="file"></param>
    /// <returns></returns>
    public static T Load(string folder, string file)
    {
        // get the data path of this save data
        string dataPath = GetFilePath(folder, file);

        // if the file path or name does not exist, return the default SO
        if (!Directory.Exists(Path.GetDirectoryName(dataPath)))
        {
            Debug.LogWarning("File or path does not exist! " + dataPath);
            return default(T);
        }

        // load in the save data as byte array
        byte[] jsonDataAsBytes = null;

        try
        {
            jsonDataAsBytes = File.ReadAllBytes(dataPath);
            Debug.Log("<color=green>Loaded all data from: </color>" + dataPath);
        }
        catch (Exception e)
        {
            Debug.LogWarning("Failed to load data from: " + dataPath);
            Debug.LogWarning("Error: " + e.Message);
            return default(T);
        }

        if (jsonDataAsBytes == null)
            return default(T);

        // convert the byte array to json
        string jsonData;

        // convert the byte array to json
        jsonData = Encoding.ASCII.GetString(jsonDataAsBytes);

        // convert to the specified object type
        T returnedData = JsonUtility.FromJson<T>(jsonData);

        // return the casted json object to use
        return (T)Convert.ChangeType(returnedData, typeof(T));
    }

    private static string GetFilePath(string FolderName, string FileName = "")
    {
        string filePath;
        filePath = Path.Combine(Application.streamingAssetsPath, FolderName);
        filePath = Path.Combine(filePath, FileName);
        return filePath;
    }
}