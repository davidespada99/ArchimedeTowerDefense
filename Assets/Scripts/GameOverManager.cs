using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
//public static GameOverManager main;
//public GameObject GameOverPanel;
public GameObject Mask;

public void GameOver()
{
    gameObject.SetActive(true);
    Mask.SetActive(true);
    Time.timeScale = 0; // Pauses the game
    Debug.Log("gameover panel");
}

public void Home(){
    SoundManager.Instance.PlayEffects("Button_Sound");
    Time.timeScale = 1;
    ScenesManager.instance.LoadScene(0);
}

}
