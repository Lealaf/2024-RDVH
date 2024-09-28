using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DocumentInformationObject : MonoBehaviour
{
    [SerializeField]
    Image image;

    [SerializeField]
    TextMeshProUGUI objectName;

    [SerializeField]
    TextMeshProUGUI objectDescriptionText;




    public void Hydrate(Sprite sprite, string name, string descriptionText)
    {
        image.sprite = sprite;
        this.objectName.text = name;
        this.objectDescriptionText.text = descriptionText;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



}
