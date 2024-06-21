using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{   

    public static ScenesManager instance;
    public enum Scenes{
        MainMenu,
        Level01
    }

    private void Awake(){
        if(instance != null) return;
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene(int scene){
         SceneManager.LoadScene(scene);
    }

    public void Foo(){
        Debug.Log("GAY!!!");
    }



}
