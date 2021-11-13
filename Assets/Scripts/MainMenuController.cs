using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    GameObject LevelSelection_Box;
    public void StartGame()
    {
        //SceneManager.LoadScene("Level_1");
        LevelSelection_Box.SetActive(true);
    }
    public void ExitLevelSelection()
    {
        LevelSelection_Box.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
