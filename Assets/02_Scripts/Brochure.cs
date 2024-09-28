using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Brochure : MonoBehaviour
{
    [SerializeField]
    Image image;

    [SerializeField]
    TextMeshProUGUI objectName;

    [SerializeField]
    TextMeshProUGUI objectDescriptionText;

    [SerializeField]
    UnityEvent PutBackEvent;

    [SerializeField]
    UnityEvent<int> ReportEvent;

    int idObject;


    public void Hydrate(int id, Sprite sprite, string name, string descriptionText)
    {
        this.idObject = id;
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

    public void PutBack()
    {
        PutBackEvent?.Invoke();
    }

    public void Report()
    {
        ReportEvent?.Invoke(idObject);
    }

}
