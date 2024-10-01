using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static AudioManager;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    public static GameManager Instance => instance;

    [SerializeField]
    MenuManager menuManager;


    private void Awake()
    {
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



    void Start()
    {
        DataBase.LoadData();
        GameState.Init();

        /* Changer le menu manager en singleton */
        if (menuManager == null)
        {
            menuManager = GameObject.FindGameObjectsWithTag("MenuManager")[0].GetComponentInChildren<MenuManager>();
        }

        StartMenu();
    }

    public void ResetGame()
    {
        Debug.Log("GameManager ResetGame");
        GameState.Reset();
        EventManager.Instance.ShowAllObjects.Invoke();

        AudioManager.Instance.StopAmbiant();
        AudioManager.Instance.PlayMusic(music.menu);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        Debug.Log("Start Game");
        AudioManager.Instance.PlayMusic(music.game1);
        AudioManager.Instance.PlayAmbiant(ambiant.inside);
        menuManager.ShowInGameMenu();
    }

    public void ExitGame()
    {
        List<string> list = new List<string>(GameState.collected);
        menuManager.ShowEndMenu(GameState.score.ToString(), GameState.score > 3, list);
    }

    public void StartMenu()
    {
        AudioManager.Instance.StopAmbiant();
        AudioManager.Instance.PlayMusic(music.menu);
        menuManager.ShowStartMenu();
    }
}
