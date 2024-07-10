using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPanelManager : MonoBehaviour
{
    public static TutorialPanelManager instance;

    public GameObject[] TutorialPanels;

    public GameObject[] ContinueButtons;

    public GameObject[] Hands;
    // Start is called before the first frame update

    void Awake(){
        
        if (instance == null) instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    public void ToggleTutorialPanel(int index)
    {
        TutorialPanels[index].SetActive(!TutorialPanels[index].activeSelf);
    }

    public void ToggleTutorialHand(int index)
    {
        Hands[index].SetActive(!Hands[index].activeSelf);
    }

    public void Continue(){
        TutorialManager.instance.ResumeGame();
        SoundManager.Instance.PlayEffects("Button_Sound");
    }

    internal void Reset()
    {
        for(int i = 0; i < TutorialPanels.Length; i++ ){
            TutorialPanels[i].SetActive(false);
        }
        for(int i = 0; i < Hands.Length; i++ ){
            Hands[i].SetActive(false);
        }
    }
}
