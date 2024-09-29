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
    TextMeshProUGUI scoreText;

    [SerializeField]
    Transform contentBadItem;

    [SerializeField]
    Image prefabeImageItem;

    
    public void Hydrate(string score, string textScore, List<Sprite> listSprite)
    {
        this.score.text = score;
        this.scoreText.text = textScore;

        foreach (Sprite sprite in listSprite)
        {
            var item= Instantiate<Image>(prefabeImageItem, contentBadItem);
            item.sprite = sprite;
        }
    }
}
