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
        if (collected == null) {
            collected = new HashSet<string>();
            EventManager.Instance.CollectedObjectsUpdated.Invoke();
        }
    }

    public static void CollectObject(string id)
    {
        collected.Add(id);
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
            Debug.Log("found");
            var obj = DataBase.data.objects.Find(o => o.ID == id);
            Debug.Log(obj.type);
            if (obj.type == "anachronique") {
                return true;
            } else {
                return false;
            }
        } else {
            Debug.Log("not found");
            return false;
        }

    }

    public static int GetScore()
    {
        if (collected == null) {
            return 0;
        }
        var all = 0;
        var good = 0;
        var bad = 0;
        foreach(var id in collected)
        {
            all++;
            if (IsAnachronic(id))
                good++;
            else
                bad++;
        }
        Debug.Log("all = " + all);
        Debug.Log("good = " + good);
        Debug.Log("bad = " + bad);

        return good - bad;
    }
}
