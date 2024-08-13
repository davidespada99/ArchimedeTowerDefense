using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{

    public static TutorialManager instance;

    public static bool firstTurretCanBePlaced = false;

    public static bool firstTurretClicked = false;

    public static bool runTutorial = true;
    private int state = 0;
    // Start is called before the first frame update

    private void Awake(){

        if (instance == null)
        {
            
            instance = this;
           
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public int GetState(){
        return state;
    }

    public bool isTutorialRunning(){
        return runTutorial;
    }
    public void ResetTutorial(){
        TutorialPanelManager.instance.Reset();
        state = 0;
        runTutorial = true;
        firstTurretCanBePlaced = false;
        firstTurretClicked = false;
        Debug.Log("RESET TUTORIAL - state: " + state + " - runTutorial: " + runTutorial );

    }
    public void PauseGame(){
        Time.timeScale = 0f;
    }

    public void ResumeGame(){
        Time.timeScale = 1f;
        state++;

        switch(state){
            case 1: 
                BlinkHandler.instance.ToggleBlink(0); // Blink the health
                if(!ScenesManager.instance.IsSwapped()){
                    TutorialPanelManager.instance.ToggleTutorialHand(4);
                }else{
                    TutorialPanelManager.instance.ToggleTutorialHand(5);
                }
            break;
            case 3: 
                BlinkHandler.instance.ToggleBlink(1);    //Blink the coin
                if(!ScenesManager.instance.IsSwapped()){
                    TutorialPanelManager.instance.ToggleTutorialHand(6);
                }else{
                    TutorialPanelManager.instance.ToggleTutorialHand(7);
                } 
                break;
            case 4: 
                BlinkHandler.instance.ToggleBlink(2); //Blink the wave
                if(!ScenesManager.instance.IsSwapped()){
                    TutorialPanelManager.instance.ToggleTutorialHand(8);
                }else{
                    TutorialPanelManager.instance.ToggleTutorialHand(9);
                } 
           
                break;

            case 5:
                SellPanelManager.main.Pause();
                runTutorial = false; 
                Debug.Log("TUTORIAL TERMINATO");
                break;
            
        }

    } 
   
   public void RunTutorialStep(){ // viene chiamato ogni volta che c’e un evento
   Debug.Log("RUN TUTORIAL BOOL VAL: " + runTutorial);
   Debug.Log("Tutorial state: " + state);
	if (!runTutorial) return;
    PauseGame();
	switch(state){
		// Quando il giocatore preme start e parte la mappa ce tutta la spiegazione
		case 0:  
            BlinkHandler.instance.ToggleBlink(0); 
            TutorialPanelManager.instance.ToggleTutorialPanel(0); 
            if(!ScenesManager.instance.IsSwapped()){
                TutorialPanelManager.instance.ToggleTutorialHand(4);
            }else{
                TutorialPanelManager.instance.ToggleTutorialHand(5);
            }
            
            break;
		case 1: 
            firstTurretCanBePlaced = true;
            BlinkHandler.instance.ToggleBlink(3);
            TutorialPanelManager.instance.ToggleTutorialPanel(1); 
            if(!ScenesManager.instance.IsSwapped()){
                TutorialPanelManager.instance.ToggleTutorialHand(0);    
            }else{
                TutorialPanelManager.instance.ToggleTutorialHand(1);
            }
            break; // ci dovrebbe essere una coroutine per far aspettare 1 secondo
		case 2: //Tutorial step per mostrare la moneta
            BlinkHandler.instance.ToggleBlink(1);
            TutorialPanelManager.instance.ToggleTutorialPanel(2); 
            if(!ScenesManager.instance.IsSwapped()){
                TutorialPanelManager.instance.ToggleTutorialHand(6);
            }else{
                TutorialPanelManager.instance.ToggleTutorialHand(7);
            } 
            break;
		case 3: 
            BlinkHandler.instance.ToggleBlink(2); 
            TutorialPanelManager.instance.ToggleTutorialPanel(3); 
            if(!ScenesManager.instance.IsSwapped()){
                TutorialPanelManager.instance.ToggleTutorialHand(8);
            }else{
                TutorialPanelManager.instance.ToggleTutorialHand(9);
            } 
            break;
        case 4:
            TutorialPanelManager.instance.ToggleTutorialPanel(4);
            break;
        
        
}
			
}

}
