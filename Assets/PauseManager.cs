using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject PauseMenu;

    public void Pause()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0; // Pauses the game
        Debug.Log("pause");
    }

    public void Continue()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1; // Resumes the game
    }

    public void Home(){
        Time.timeScale = 1;
        ScenesManager.instance.LoadScene(0);
    }
}
