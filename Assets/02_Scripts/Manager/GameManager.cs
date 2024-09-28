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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        menuManager.ShowInGameMenu();
    }

    public void ExitGame()
    {

    }
}
