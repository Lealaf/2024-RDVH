using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject topMenu;

    [SerializeField]
    GameObject optionMenu;

    [SerializeField]
    Brochure brochure;

    [SerializeField]
    DocumentInformationObject carnet;


    [SerializeField]
    GameObject menuBrochure;
    [SerializeField]
    GameObject menuCarnet;

    public void OpenOptionMenu( bool open)
    {
        topMenu.SetActive(!open);
        optionMenu.SetActive(open);
    }
    // Start is called before the first frame update
    void Start()
    {
        CloseAll();
        
    }

    public void OpenCarnet(bool open)
    {
        if (open)
        {
            string id = "OBJECT_1";
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
            carnet.Hydrate(sprite, obj.nom, obj.description);
        }
        carnet.gameObject.SetActive(open);
        menuCarnet.SetActive(!open);

    }

    public void OpenBrochure(bool open)
    {
        brochure.gameObject.SetActive(open);
        menuBrochure.SetActive(!open);
    }

    public void CloseAll()
    {
        OpenCarnet(false);
        OpenBrochure(false);
        OpenOptionMenu(false);
    }
}
