using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    MenuManager menuManager;
    // Start is called before the first frame update
    void Start()
    {
        menuManager.ShowStartMenu();
        DataBase.LoadData();
        GameState.Init();
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

    }
}
