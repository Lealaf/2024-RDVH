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

    [SerializeField]
    TextMeshProUGUI nbClueText;

    [SerializeField]
    Button clueButton;


    int pageNumber;

    void Start()
    {
        EventManager.Instance.CurrentNbCluesUpdated.AddListener(UpdateDisplay);
    }

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
                UpdateDisplay();
            }
        }
    }


    public void IncreaseObjectClueLevel()
    {
        ObjectInfo currentObject = DataBase.data.objects[pageNumber];
        GameState.IncreaseObjectClueLevel(currentObject.ID);
    }

    public string RemoveBoldTags(string input)
    {
        // Utilise une expression régulière pour supprimer les balises <b> et </b>
        return Regex.Replace(input, "<[/]?b>", "");
    }


    public void UpdateDisplay()
    {
        ObjectInfo obj = DataBase.data.objects[pageNumber];
        int clueLevel = GameState.objectsClueLevels[obj.ID];

        image.sprite = DataBase.sprites[obj.ID];
        image.preserveAspect = true;
        this.objectName.text = obj.nom;
        this.objectDescriptionText.text =
            clueLevel >= 1
            ? obj.description
            : RemoveBoldTags(obj.description);
        this.creator.text = 
            clueLevel >= 2
            ? obj.inventeur
            : "?!?";
        this.date.text =  
            clueLevel >= 3
            ? obj.date
            : "?!?";

        nbClueText.text = GameState.currentNbClues.ToString();
        if (GameState.MaxClueLevelForObject(obj.ID) || GameState.currentNbClues == 0) {
            clueButton.interactable = false;
        } else {
            clueButton.interactable = true;
        }
    }
}
