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
    DocumentInformationObject carnet;



    [SerializeField]
    UnityEvent PutBackEvent;

    [SerializeField]
    public UnityEvent<string> ReportEvent;

    string idObject;

    [SerializeField]
    GameObject viewver;


    void Start()
    {
        EventManager.Instance.SelectObject.AddListener(HydrateAndShow);
    }

    //TODO passer en parametre la classe avec tt les info pour hydrater tt
    public void HydrateAndShow(string id)
    {
        this.idObject = id;
        Show(true);
    }


    public void PutBack()
    {
        Show(false);
        PutBackEvent?.Invoke();

    }

    public void Report()
    {
        Show(false);
        GameState.CollectObject(idObject);
        EventManager.Instance.CollectObject.Invoke(idObject);
    }

    public void Show(bool show)
    {
        canvasGroup.alpha = show?1:0;
        canvasGroup.interactable = show;
    }



}
