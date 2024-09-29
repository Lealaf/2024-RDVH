using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;


public class GameState
{
    public static HashSet<string> collected;

    public static void Init()
    {
        collected = new HashSet<string>();
        EventManager.Instance.CollectedObjectsUpdated.Invoke();
    }

    public static void CollectObject(string id)
    {
        collected.Add(id);
        EventManager.Instance.CollectedObjectsUpdated.Invoke();
    }

    public static int NumberOfCollectedObjects()
    {
        return collected.Count;
    }
}
