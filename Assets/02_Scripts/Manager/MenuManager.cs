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

    public void ShowEndMenu(string score, string textScore, List<Sprite> listSprite)
    {
        endMenu.Hydrate(score, textScore, listSprite);
        endMenu.gameObject.SetActive(true);
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
