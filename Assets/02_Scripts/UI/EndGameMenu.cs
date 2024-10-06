using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndGameMenu : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI score;

    [SerializeField]
    TextMeshProUGUI remainsToBeFound;

    [SerializeField]
    TextMeshProUGUI cluesUsed;


    [SerializeField]
    GameObject goodText;

    [SerializeField]
    GameObject badText;

    [SerializeField]
    Transform contentBadItem;

    [SerializeField]
    ItemResult prefabeImageItem;

    [Header("Answer Show")]

    [SerializeField]
    GameObject goodAnswer;

    [SerializeField]
    GameObject badAnswer;

    [SerializeField]
    Image imageShow;

    [SerializeField]
    TextMeshProUGUI objectName;

    [SerializeField]
    TextMeshProUGUI creator;

    [SerializeField]
    TextMeshProUGUI date;

    [SerializeField]
    TextMeshProUGUI objectDescriptionText;


    public void Hydrate(string score, string remainsToBeFound, string cluesUsed, bool goodScore, List<string> listString)
    {
        this.score.text = score;
        goodText.SetActive(goodScore);
        badText.SetActive(!goodScore);

        ItemResult firstItem = null;
        foreach (string id in listString)
        {
            var item = Instantiate<ItemResult>(prefabeImageItem, contentBadItem);

            if (item == null) {
                firstItem = item;
            }

            List<Sprite> sprites = new List<Sprite>();
            var sprite = DataBase.vignettesSprites.ContainsKey(id) ? DataBase.vignettesSprites[id] : null;
            Debug.LogError("TODO");
            if (DataBase.data.objects.Exists(o => o.ID == id))
            {
                var obj = DataBase.data.objects.Find(o => o.ID == id);
                item.Hydrate(id, sprite, GameState.IsAnachronic(id), null, obj.siecle, obj.description);
            }
            else
            {
                item.Hydrate(id, sprite, GameState.IsAnachronic(id), null, null, null);
            }

            var button = item.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.AddListener(() => ChangeShwoAnswer(item));
            }
        }

        if(firstItem != null)
        {
            ChangeShwoAnswer(firstItem);
        }
    }
    

    public void ChangeShwoAnswer(ItemResult itemResult)
    {
        
        goodAnswer.SetActive(itemResult.isGoodAnswer);
        badAnswer.SetActive(!itemResult.isGoodAnswer);

        imageShow.sprite = itemResult.sprite;
        objectName.text = itemResult.nameElem.text;
        creator.text = itemResult.creator;
        date.text = itemResult.date;
        objectDescriptionText.text = itemResult.objectDescriptionText;


    }


}
