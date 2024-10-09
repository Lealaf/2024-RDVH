using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;


public class GameState
{
    public static HashSet<string> collected;
    public static int score = 0;
    public static int nAnachronic = 0;
    public static int nNonAnachronic = 0;
    public static int maxNbClues = 7;
    public static int currentNbClues;
    public static int maxClueLevel = 3;
    public static int startingClueLevel = 0;
    public static Dictionary<string, int> objectsClueLevels;

    public static void Init()
    {
        if (collected == null) {
            collected = new HashSet<string>();
            EventManager.Instance.CollectedObjectsUpdated.Invoke();
        }
        SetCurrentNbClues(maxNbClues);
        if (objectsClueLevels == null)
        {
            objectsClueLevels = new Dictionary<string, int>();
            foreach (var obj in DataBase.data.objects)
            {
                objectsClueLevels.Add(obj.ID, startingClueLevel);
            }
        }
    }

    public static void Reset()
    {
        collected.Clear();
        ComputeScore();
        EventManager.Instance.CollectedObjectsUpdated.Invoke();
        SetCurrentNbClues(maxNbClues);
        foreach (var key in objectsClueLevels.Keys.ToList())
        {
            objectsClueLevels[key] = startingClueLevel;
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

    public static int GetNbAnachronicFound()
    {
        nAnachronic = collected.Count(id => IsAnachronic(id));
        return nAnachronic;
    }

    public static void SetCurrentNbClues(int nbClues)
    {
        currentNbClues = nbClues;
        EventManager.Instance.CurrentNbCluesUpdated.Invoke();
    }

    public static int GetObjectClueLevel(string ID)
    {
        return objectsClueLevels[ID];
    }

    public static bool MaxClueLevelForObject(string ID)
    {
        return objectsClueLevels[ID] == maxClueLevel;
    }

    public static void IncreaseObjectClueLevel(string ID)
    {
        if (currentNbClues > 0 && !MaxClueLevelForObject(ID)) {
            int value;
            objectsClueLevels.TryGetValue(ID, out value);
            objectsClueLevels[ID] = value + 1;
            EventManager.Instance.ObjectClueLevelUpdated.Invoke(ID);
            SetCurrentNbClues(currentNbClues - 1);
        }
    }
}
