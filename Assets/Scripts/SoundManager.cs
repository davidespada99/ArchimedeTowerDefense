using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager Instance;

    public  Sound[] musicSounds, effectsSounds;
    public AudioSource musicSource, effectsSource;


    private void Awake(){
        if(Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }
    }

    private void Start(){
        PlayMusic("Intro");
    }
    public void PlayMusic(string name){
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if (s == null) Debug.Log("Not Found"); 
        else{
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlayEffects(string name){
    Sound s = Array.Find(effectsSounds, x => x.name == name);

    if (s == null) Debug.Log("Not Found"); 
    else{
        effectsSource.clip = s.clip;
        effectsSource.PlayOneShot(s.clip);
    }
}

    public void PauseSound(){
        AudioListener.pause = true;
    }

    public void PlaySound(){
        AudioListener.pause = false;
    }




}
