using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class CheckObjectMenu : MonoBehaviour
{
    [SerializeField]
    CanvasGroup canvasGroup;



    [SerializeField]
    UnityEvent PutBackEvent;

    [SerializeField]
    public UnityEvent<string> ReportEvent;

    string idObject;

    [SerializeField]
    GameObject viewver;


    


    public void Hydrate(DisplayedObject obj)
    {
        this.idObject = obj.objectName;
    }


    public void PutBack()
    {
        PutBackEvent?.Invoke();
        AudioManager.Instance.putDownObjectNoise();
    }

    public void Report()
    {
        GameState.CollectObject(idObject);
        EventManager.Instance.CollectObject.Invoke(idObject);
        AudioManager.Instance.takeObjectNoise();
    }



}
