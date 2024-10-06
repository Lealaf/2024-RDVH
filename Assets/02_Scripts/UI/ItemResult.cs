using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemResult : MonoBehaviour
{

    [SerializeField]
    public TextMeshProUGUI nameElem;

    [SerializeField]
    Image image;

    [SerializeField]
    GameObject good;

    [SerializeField]
    GameObject bad;

    [NonSerialized]
    public bool isGoodAnswer;

    [NonSerialized]
    public Sprite sprite;

    [NonSerialized]
    public string creator;

    [NonSerialized]
    public string date;

    [NonSerialized]
    public string objectDescriptionText;

    public void Hydrate(string name, Sprite sprite , bool isgood, string creator, string date, string objectDescriptionText)
    {
        this.nameElem.text = name;
        this.sprite = sprite;
        image.sprite = sprite;

        good.SetActive(isgood);
        bad.SetActive(!isgood);

        isGoodAnswer = isgood;

        this.creator = creator;
        this.date = date;
        this.objectDescriptionText = objectDescriptionText;
    }
}
