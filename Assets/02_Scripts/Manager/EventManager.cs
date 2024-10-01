using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static EventManager;

public class EventManager : MonoBehaviour
{
    private static EventManager instance = null;
    public static EventManager Instance => instance;

    public UnityEvent<DisplayedObject> SelectObject;
    public UnityEvent<string> CollectObject;
    public UnityEvent CollectedObjectsUpdated;
    public UnityEvent ShowAllObjects;

    private void Awake()
    {
        Debug.Log("Awake EventManager");
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
