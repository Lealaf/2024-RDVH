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
    GameObject scoreText;

    [SerializeField]
    GameObject goodText;

    [SerializeField]
    GameObject badText;

    [SerializeField]
    Transform contentBadItem;

    [SerializeField]
    ItemResult prefabeImageItem;

    
    public void Hydrate(string score, bool goodScore, List<string> listString)
    {
        this.score.text = score;
        goodText.SetActive(goodScore);
        badText.SetActive(!goodScore);


        //this.scoreText.text = textScore;

        foreach (string id in listString)
        {
            var item= Instantiate<ItemResult>(prefabeImageItem, contentBadItem);


            List<Sprite> sprites = new List<Sprite>();
            var sprite = DataBase.vignettesSprites.ContainsKey(id) ? DataBase.vignettesSprites[id] : null;
            item.Hydrate(id, sprite, GameState.IsAnachronic(id));
        }
    }
    public void Replay()
    {
        // AudioManager.Instance.PlayMusic(music.menu);
        // AudioManager.Instance.StopAmbiant();
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
