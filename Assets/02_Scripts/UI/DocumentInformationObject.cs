using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DocumentInformationObject : MonoBehaviour
{
    [SerializeField]
    GameObject bookCover;

    [SerializeField]
    GameObject openCarnetGameObjrct;

    [SerializeField]
    Image image;

    [SerializeField]
    TextMeshProUGUI objectName;

    [SerializeField]
    TextMeshProUGUI creator;

    [SerializeField]
    TextMeshProUGUI date;

    [SerializeField]
    TextMeshProUGUI objectDescriptionText;

    int pageNumber;


    public void Open()
    {
        GoToPage(-1);
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
                openCarnetGameObjrct.SetActive(false);
                bookCover.SetActive(true);
                pageNumber = -1;

            }
            else
            {
                openCarnetGameObjrct.SetActive(true);
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

    public string RemoveBoldTags(string input)
    {
        // Utilise une expression régulière pour supprimer les balises <b> et </b>
        return Regex.Replace(input, "<[/]?b>", "");
    }

    public void Hydrate(Sprite sprite, string name, string descriptionText)
    {
        image.sprite = sprite;
        this.objectName.text = name;
        // C'est ici que ça se passe pour gérer l'affchage des textes en gras en
        // fonction de l'activation des indices.
        bool displayBoldText = true;
        if (displayBoldText) {
            this.objectDescriptionText.text = descriptionText;
        } else {
            this.objectDescriptionText.text = RemoveBoldTags(descriptionText);
        }
    }

    public void Hydrate(Sprite sprite, string name, string creator, string date, string descriptionText)
    {
        this.creator.text = creator;
        this.date.text = date;
        Hydrate(sprite, name, descriptionText);
    }



}
