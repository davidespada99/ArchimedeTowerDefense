using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{   

    public static ScenesManager instance;
    public static bool swapped = false;

    public static bool sound = true;

    public enum Scenes{
        MainMenu,
        Level01
    }


    public bool IsSwapped(){
        return swapped;
    }

    public bool IsSound(){
        return sound;
    }

    private void Awake(){
        if(instance != null) return;
        instance = this;
    }

    public void LoadScene(int scene){
         SceneManager.LoadScene(scene);
    }

    public void Swap(){
        swapped = !swapped;
    }

    public void SwitchSound(){
        sound = !sound;
    }

    public void Quit(){
        Application.Quit();
    }

}
