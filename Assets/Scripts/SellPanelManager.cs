using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellPanelManager : MonoBehaviour
{
    public static SellPanelManager main;
    private GameObject turret; 
    public GameObject SellMenu;
    public GameObject Mask;

    

    private void Awake(){
        Debug.Log("AWAKE SELL MANAGER");
         if (main == null)
        {
            main = this;
        }
        else if (main != this)
        {
            Destroy(gameObject);
        } 
        
    }

    public void SetTurret(GameObject gm){
        turret = gm;
    }
    public void Pause()
    {
        SoundManager.Instance.PlayEffects("Button_Sound");
        SellMenu.SetActive(true);
        Mask.SetActive(true);
        Time.timeScale = 0; // Pauses the game
        Debug.Log("sell panel");
    }

     public void Sell(){
        LevelManager.main.IncreaseCurrency( (int)(turret.GetComponent<Turret>().GetCost() / 2));
        Destroy(turret);  
        Continue();
    }

    public void Continue()
    {
        SoundManager.Instance.PlayEffects("Button_Sound");
        SellMenu.SetActive(false);
        Mask.SetActive(false);
        Time.timeScale = 1; // Resumes the game
    }


}