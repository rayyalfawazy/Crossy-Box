using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] TMP_Text gameversion;
    public void StartPlay()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void Start()
    {
        if (gameversion != null) 
        {
            gameversion.text = "Version " + Application.version;
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
