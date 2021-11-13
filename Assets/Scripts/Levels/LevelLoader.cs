using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Button))]
public class LevelLoader : MonoBehaviour
{
    public string LevelName;
    Button LevelLoad_btn;

    void Awake()
    {
        LevelLoad_btn = GetComponent<Button>();
        LevelLoad_btn.onClick.AddListener(LoadLevel);
    }
    public void LoadLevel()
    {
        SceneManager.LoadScene(LevelName);
    }
}
