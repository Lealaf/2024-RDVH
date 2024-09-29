using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFonction : MonoBehaviour
{

    public void ExitGame()
    {
        GameManager.Instance.ExitGame();
    }

    public void StartGame()
    {
        GameManager.Instance.StartGame();
    }

    public void ResetGame()
    {
        GameManager.Instance.ResetGame();
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
