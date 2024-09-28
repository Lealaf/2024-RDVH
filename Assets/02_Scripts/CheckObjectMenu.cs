using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckObjectMenu : MonoBehaviour
{
    [SerializeField]
    CanvasGroup canvasGroup;

    [SerializeField]
    DocumentInformationObject brochure;

    [SerializeField]
    DocumentInformationObject carnet;

    [SerializeField]
    GameObject viewver;

    [SerializeField]
    UnityEvent PutBackEvent;

    [SerializeField]
    UnityEvent<int> ReportEvent;

    int idObject;

    public void OpenCarnet(bool open)
    {
        carnet.gameObject.SetActive(open);

    }

    public void OpenBrochure(bool open)
    {
        brochure.gameObject.SetActive(open);
    }

    public void CloseAll()
    {
        OpenCarnet(false);
        OpenBrochure(false);
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
    }

    //TODO passer en parametre la classe avec tt les info pour hydrater tt
    public void HydrateAndShow( int id)
    {
        this.idObject = id;
        //brochure.Hydrate()
        //carnet.Hydrate()

        OpenCarnet(false);
        OpenBrochure(true);

        //TODO
        //viewver.SetActive(true);

        canvasGroup.alpha = 1.0f;
        canvasGroup.interactable = true;
    }


    public void PutBack()
    {
        PutBackEvent?.Invoke();
    }

    public void Report()
    {
        ReportEvent?.Invoke(idObject);
    }

    private void Start()
    {
        CloseAll();
    }

}
