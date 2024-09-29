using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemResult : MonoBehaviour
{

    [SerializeField]
    TextMeshProUGUI nameElem;

    [SerializeField]
    Image image;

    [SerializeField]
    GameObject good;

    [SerializeField]
    GameObject bad;

    public void Hydrate(string name, Sprite sprite , bool idgood)
    {
        this.nameElem.text = name;
        image.sprite = sprite;

        good.SetActive(idgood);
        bad.SetActive(!idgood);
    }
}
