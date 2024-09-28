using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Brochure : MonoBehaviour
{
    [SerializeField]
    Image image;

    public void Hydrate(Sprite sprite)
    {
        image.sprite = sprite;
    }

}
