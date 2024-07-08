using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{   

    public static ScenesManager instance;
    public static bool swapped ;
    public static bool sound ;


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

    void Start(){
       
    }

    private void Awake(){

        if(instance != null) return;
        instance = this;
        UpdateCaches();
    }

    public void UpdateCaches(){
       

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
