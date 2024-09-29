using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject startMenu;

    [SerializeField]
    GameObject inGameMenu;

    [SerializeField]
    GameObject pauseMenu;

    [SerializeField]
    GameObject endMenu;


    public void ShowInGameMenu()
    {
        endMenu.SetActive(false);
        startMenu.SetActive(false);
        pauseMenu.SetActive(false);
        inGameMenu.SetActive(true);
    }

    public void ShowPauseMenu() 
    {
        endMenu.SetActive(false);
        startMenu.SetActive(false);
        pauseMenu.SetActive(true);
        inGameMenu.SetActive(false);
    }


    public void ShowStartMenu()
    {
        endMenu.SetActive(false);
        startMenu.SetActive(true);
        pauseMenu.SetActive(false);
        inGameMenu.SetActive(false);
    }

    public void ShowEndMenu()
    {
        endMenu.SetActive(true);
        startMenu.SetActive(false);
        pauseMenu.SetActive(false);
        inGameMenu.SetActive(false);
    }

    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        
    }
}
