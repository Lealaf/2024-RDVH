using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static AudioManager;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    public static GameManager Instance => instance;

    private void Awake()
    {
        Debug.Log("Awake game manager");
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
        StartMenu();
    }

    public void ResetGame()
    {
        Debug.Log("GameManager ResetGame");
        GameState.Reset();
        EventManager.Instance.ShowAllObjects.Invoke();
        StartMenu();
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
        MenuManager.Instance.ShowInGameMenu();
    }

    public void ExitGame()
    {
        List<string> list = new List<string>(GameState.collected);
        Debug.LogError("TODO");
        MenuManager.Instance.ShowEndMenu(GameState.GetNbAnachronicFound().ToString(), (9-GameState.GetNbAnachronicFound()).ToString(), GameState.GetNbCluesLeft().ToString(), GameState.score > 3, list);
    }

    public void StartMenu()
    {
        Debug.Log("Start Menu");
        AudioManager.Instance.StopAmbiant();
        AudioManager.Instance.PlayMusic(music.menu);
        MenuManager.Instance.ShowStartMenu();
    }
}
