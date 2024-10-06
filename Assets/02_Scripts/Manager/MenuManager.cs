using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AudioManager;

public class MenuManager : MonoBehaviour
{
    private static MenuManager instance = null;
    public static MenuManager Instance => instance;

    [SerializeField]
    GameObject startMenu;

    [SerializeField]
    GameObject inGameMenu;

    [SerializeField]
    GameObject pauseMenu;

    [SerializeField]
    EndGameMenu endMenu;


    private void Awake()
    {
        Debug.Log("Awake menu manager");
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
    }

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

    public void ShowEndMenu(string score, string remainsToBeFound, string cluesUsed, bool goodScore, List<string> listString)
    {
        endMenu.Hydrate(score, remainsToBeFound, cluesUsed, goodScore, listString);
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
