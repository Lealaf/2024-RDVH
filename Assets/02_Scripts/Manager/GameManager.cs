using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (menuManager == null)
        {
            menuManager = GameObject.FindGameObjectsWithTag("MenuManager")[0].GetComponentInChildren<MenuManager>();
        }
        menuManager.ShowStartMenu();
    }

    public void ResetGame()
    {
        GameState.Reset();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        AudioManager.Instance.PlayMusic(AudioManager.music.game1);
        AudioManager.Instance.PlayAmbiant(AudioManager.ambiant.inside);
        menuManager.ShowInGameMenu();
    }

    public void ExitGame()
    {
        List<Sprite> sprites = new List<Sprite>(); //TODO
        menuManager.ShowEndMenu(GameState.score.ToString(), GameState.score.ToString(), sprites);
    }
}
