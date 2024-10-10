using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static AudioManager;

public class InGameMenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject exploreMenu;

    [SerializeField]
    GameObject optionMenu;

    [SerializeField]
    Brochure brochure;

    [SerializeField]
    DocumentInformationObject carnet;

    [SerializeField]
    CheckObjectMenu checkMenu;


    [SerializeField]
    GameObject menuBrochure;
    [SerializeField]
    GameObject menuCarnet;

    public void OpenOptionMenu( bool open)
    {
        //checkMenu.gameObject.SetActive(!open);
        exploreMenu.SetActive(!open);
        optionMenu.SetActive(open);
    }
    // Start is called before the first frame update
    void Start()
    {
        Init();
        EventManager.Instance.SelectObject.AddListener(OpenAndHydrate);

    }

    public void OpenAndHydrate(DisplayedObject obj)
    {
        checkMenu.gameObject.SetActive(true);
        exploreMenu.SetActive(false);
        checkMenu.Hydrate(obj);

    }

    public void CloseCheckObjectMenu()
    {
        checkMenu.gameObject.SetActive(false);
        exploreMenu.SetActive(true);
    }

    public void OpenCarnet(bool open)
    {
        if (open)
        {
            carnet.Open();
        }
        else
        {
            AudioManager.Instance.closeBookNoise();
        }
        carnet.gameObject.SetActive(open);
        menuCarnet.SetActive(!open);

    }

    public void OpenBrochure(bool open)
    {
        brochure.gameObject.SetActive(open);
        menuBrochure.SetActive(!open);
    }

    public void Init()
    {
        OpenCarnet(false);
        OpenBrochure(false);
        OpenOptionMenu(false);
        CloseCheckObjectMenu();
    }

    public void QuitGame()
    {
        GameManager.Instance.ResetGame();
    }
}
