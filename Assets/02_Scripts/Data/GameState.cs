using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;


public class GameState
{
    public static HashSet<string> collected;
    public static int score = 0;
    public static int nAnachronic = 0;
    public static int nNonAnachronic = 0;
    
    public static void Init()
    {
        if (collected == null) {
            collected = new HashSet<string>();
            EventManager.Instance.CollectedObjectsUpdated.Invoke();
        }
    }

    public static void CollectObject(string id)
    {
        collected.Add(id);
        ComputeScore();
        EventManager.Instance.CollectedObjectsUpdated.Invoke();
    }

    public static int NumberOfCollectedObjects()
    {
        return collected == null ? 0 : collected.Count;
    }

    public static bool IsAnachronic(string id)
    {
        Debug.Log(id);
        if (DataBase.data.objects.Exists(o => o.ID == id)) {
            var obj = DataBase.data.objects.Find(o => o.ID == id);
            if (obj.type == "anachronique") {
                return true;
            } else {
                return false;
            }
        } else {
            return false;
        }

    }

    public static void ComputeScore()
    {
        if (collected == null) {
            score = 0;
        }
        var all = 0;
        nAnachronic = 0;
        nNonAnachronic = 0;
        foreach(var id in collected)
        {
            all++;
            if (IsAnachronic(id))
                nAnachronic++;
            else
                nNonAnachronic++;
        }

        score = nAnachronic - nNonAnachronic;
    }
}
