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
    public UnityEvent<string> ReportEvent;

    string idObject;

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
    public void HydrateAndShow(string id)
    {
        this.idObject = id;

        ObjectInfo obj;
        if (DataBase.data.objects.Exists(o => o.ID == id)) {
            obj = DataBase.data.objects.Find(o => o.ID == id);
        } else {
            Debug.LogError("Can't find an object which id is " + id + ". Use the first in the list to avoid a crash.");
            obj = DataBase.data.objects[0];
        }

        brochure.Hydrate(null, obj.nom, obj.description);

        var sprite = DataBase.sprites[obj.ID];
        carnet.Hydrate(sprite, obj.nom, obj.description);

        OpenCarnet(false);
        OpenBrochure(true);

        //TODO
        //viewver.SetActive(true);

        canvasGroup.alpha = 1.0f;
        canvasGroup.interactable = true;
    }


    public void PutBack()
    {
        OpenCarnet(false);
        OpenBrochure(false);
        PutBackEvent?.Invoke();
    }

    public void Report()
    {
        OpenCarnet(false);
        OpenBrochure(false);
        ReportEvent?.Invoke(idObject);
    }

    private void Start()
    {
        CloseAll();
    }

}
