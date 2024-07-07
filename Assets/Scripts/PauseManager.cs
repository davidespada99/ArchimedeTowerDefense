using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject PauseMenu;
    public GameObject Mask;

    public void Pause()
    {
        PauseMenu.SetActive(true);
        Mask.SetActive(true);
        Time.timeScale = 0; // Pauses the game
        Debug.Log("pause");
    }

    public void Continue()
    {
        PauseMenu.SetActive(false);
        Mask.SetActive(false);
        Time.timeScale = 1; // Resumes the game
    }

    public void Home(){
        Time.timeScale = 1;
        ScenesManager.instance.LoadScene(0);
    }
}
