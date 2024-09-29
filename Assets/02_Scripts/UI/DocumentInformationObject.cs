using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DocumentInformationObject : MonoBehaviour
{
    [SerializeField]
    GameObject bookCover;

    [SerializeField]
    Image image;

    [SerializeField]
    TextMeshProUGUI objectName;

    [SerializeField]
    TextMeshProUGUI objectDescriptionText;

    int pageNumber;


    public void Open()
    {
        pageNumber = -1;
    }

    public void NextPage()
    {
        GoToPage(pageNumber + 1);
    }

    public void PreviusPage()
    {
        GoToPage(pageNumber - 1);
    }

    public void GoToPage(int idPage)
    {
        if (DataBase.data.objects.Count > 0)
        {
            if (idPage < 0)
            {
                bookCover.SetActive(true);
                pageNumber = -1;

            }
            else
            {
                bookCover.SetActive(false);
                if (idPage >= DataBase.data.objects.Count)
                {
                    idPage = DataBase.data.objects.Count - 1;
                }

                pageNumber = idPage;
                ObjectInfo obj = DataBase.data.objects[pageNumber];
                var sprite = DataBase.sprites[obj.ID];
                Hydrate(sprite, obj.nom, obj.description);
            }
        }
    }

    public void GoToPage(string id)
    {
        ObjectInfo obj;
        if (DataBase.data.objects.Exists(o => o.ID == id))
        {
            obj = DataBase.data.objects.Find(o => o.ID == id);
        }
        else
        {
            Debug.LogError("Can't find an object which id is " + id + ". Use the first in the list to avoid a crash.");
            obj = DataBase.data.objects[0];
        }

        var sprite = DataBase.sprites[obj.ID];
        Hydrate(sprite, obj.nom, obj.description);
    }

    public void Hydrate(Sprite sprite, string name, string descriptionText)
    {
        image.sprite = sprite;
        this.objectName.text = name;
        this.objectDescriptionText.text = descriptionText;
    }



}
