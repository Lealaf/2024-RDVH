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
    public static Dictionary<string, Sprite> vignettesSprites;

    static Sprite LoadSprite(string id)
    {
        var filePath = "data/images/carnet/" + id;
        var tex = Resources.Load<Texture2D>(filePath);
        if (tex == null) {
            Debug.LogError("Can't the image for object which id is " + id + ". The expected resource shloud be\"" + filePath + "\". Use a default one to avoid a crash.");
            tex = Resources.Load<Texture2D>("data/images/carnet/not_available");
        }

        // Creates a new Sprite based on the Texture2D
        Sprite sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);

        return sprite;
    }
    static Sprite LoadVignetteSprite(string id)
    {
        var filePath = "data/images/vignettes/" + id;
        var tex = Resources.Load<Texture2D>(filePath);
        if (tex == null) {
            Debug.LogError("Can't the vignette image for object which id is " + id + ". The expected resource shloud be\"" + filePath + "\". Use a default one to avoid a crash.");
            tex = Resources.Load<Texture2D>("data/images/vignettes/not_available");
        }

        // Creates a new Sprite based on the Texture2D
        Sprite sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);

        return sprite;
    }

    public static void LoadData()
    {
        data = SaveLoad<AllData>.Load("data/data");

        // Load and cache all sprites for object images.
        sprites = new Dictionary<string, Sprite>();
        vignettesSprites = new Dictionary<string, Sprite>();
        foreach (var obj in data.objects)
        {
            Sprite sprite = LoadSprite(obj.ID);
            sprites.Add(obj.ID, sprite);
            Sprite vignetteSprite = LoadVignetteSprite(obj.ID);
            vignettesSprites.Add(obj.ID, vignetteSprite);
        }
    }
}

static class SaveLoad<T>
{    public static T Load(string resourcePath)
    {
        string jsonData;

        var jsonTextFile = Resources.Load<TextAsset>(resourcePath);
        jsonData = jsonTextFile.text;

        // convert to the specified object type
        T returnedData = JsonUtility.FromJson<T>(jsonData);

        // return the casted json object to use
        return (T)Convert.ChangeType(returnedData, typeof(T));
    }
}