using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AudioManager;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject startMenu;

    [SerializeField]
    GameObject inGameMenu;

    [SerializeField]
    GameObject pauseMenu;

    [SerializeField]
    EndGameMenu endMenu;

    public void ShowInGameMenu()
    {
        endMenu.gameObject.SetActive(false);
        startMenu.SetActive(false);
        pauseMenu.SetActive(false);
        inGameMenu.SetActive(true);
    }

    public void ShowPauseMenu() 
    {
        endMenu.gameObject.SetActive(false);
        startMenu.SetActive(false);
        pauseMenu.SetActive(true);
        inGameMenu.SetActive(false);
    }


    public void ShowStartMenu()
    {
        endMenu.gameObject.SetActive(false);
        startMenu.SetActive(true);
        pauseMenu.SetActive(false);
        inGameMenu.SetActive(false);
    }

    public void ShowEndMenu(string score, bool goodScore, List<string> listString)
    {
        endMenu.Hydrate(score, goodScore, listString);
        endMenu.gameObject.SetActive(true);
        startMenu.SetActive(false);
        pauseMenu.SetActive(false);
        inGameMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
