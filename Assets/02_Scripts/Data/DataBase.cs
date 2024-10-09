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
        Sprite sprite = Resources.Load<Sprite>(filePath);
        if (sprite == null) {
            Debug.LogError("Can't the image for object which id is " + id + ". The expected resource shloud be\"" + filePath + "\". Use a default one.");
            sprite = Resources.Load<Sprite>("data/images/carnet/not_available");
        }
        return sprite;
    }
    static Sprite LoadVignetteSprite(string id)
    {
        var filePath = "data/images/vignettes/" + id;
        var sprite = Resources.Load<Sprite>(filePath);
        if (sprite == null) {
            Debug.Log("Can't the vignette image for object which id is " + id + ". The expected resource shloud be\"" + filePath + "\". We won't display a vignette for this object.");
            return null;
        }

        return sprite;
    }

    public static void LoadData()
    {
        data = SaveLoad<AllData>.Load("data/data");

        // Sometime, there is an unexpected and hidden space character at the
        // beginning or the end of the object type. Trim the string to be robust
        // to that.
        foreach (var obj in data.objects)
        {
            obj.type = obj.type.Trim();
        }

        // Load and cache all sprites for object images.
        sprites = new Dictionary<string, Sprite>();
        vignettesSprites = new Dictionary<string, Sprite>();
        foreach (var obj in data.objects)
        {
            Sprite sprite = LoadSprite(obj.ID);
            sprites.Add(obj.ID, sprite);
            Sprite vignetteSprite = LoadVignetteSprite(obj.ID);
            if (vignetteSprite != null)
            {
                vignettesSprites.Add(obj.ID, vignetteSprite);
            }
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