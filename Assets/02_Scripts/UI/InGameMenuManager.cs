using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            carnet.Open();
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

    public void QuitGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
