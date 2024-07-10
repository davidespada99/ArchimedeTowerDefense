using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject PauseMenu;
    public GameObject Mask;

    public void Pause()
    {
        SoundManager.Instance.PlayEffects("Button_Sound");
        PauseMenu.SetActive(true);
        Mask.SetActive(true);
        Time.timeScale = 0; // Pauses the game
        Debug.Log("pause");
    }

    public void Continue()
    {
        SoundManager.Instance.PlayEffects("Button_Sound");
        PauseMenu.SetActive(false);
        Mask.SetActive(false);
        Time.timeScale = 1; // Resumes the game
    }

    public void Home(){
        SoundManager.Instance.PlayEffects("Button_Sound");
        ScenesManager.instance.LoadScene(0);
        if( TutorialManager.instance.isTutorialRunning() ){
            TutorialManager.instance.ResetTutorial();
            TurretDragHandler.setFirstTurretPlace(false);
            EnemySpawner.SetisFirstWaveEnded(false);
            EnemySpawner.SetisFirstEnemyKilled(false);
        }
        Destroy(LevelManager.main);
        Destroy(TutorialPanelManager.instance);
        Time.timeScale = 1;
    

    }
}
