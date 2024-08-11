using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour{

public static LevelManager main;
public Transform startPoint;
public Transform[] path;

public GameOverManager GameOverManager;


[Header("Attributes")]
[SerializeField] public int currency;
[SerializeField] public int life;
[SerializeField] public int waves;

private int currentWave = 1;


public void ResetCurrentWave(){
        currentWave = 1;
    }

public void IncreaseCurrentWave(){
        currentWave++;
    }

public int GetCurrentWave(){
        return currentWave;
    }

    private void Awake(){
        Debug.Log("AWAKE LEVEL MANAGER");
         if (main == null)
        {
            
            main = this;
           
            Time.timeScale = 1f;
        }
        else if (main != this)
        {
            Destroy(gameObject);
        }

        Screen.sleepTimeout = SleepTimeout.NeverSleep;  
        
    }

    void Start(){
        currency = 80;
        life = 100;
        waves = 0;

        StartCoroutine(PauseAfterEnemySpawn(0.1f));
    }

    private IEnumerator PauseAfterEnemySpawn(float delay){
        yield return new WaitForSeconds(delay); 
        TutorialManager.instance.RunTutorialStep(); 

    }


    public void IncreaseWaves(){
        waves += 1;
    }

    public void IncreaseLife(int amount = 1){
        life += amount;
    }

    public bool DecreaseLife(int amount = 1){
        if(amount < life){
            life -= amount;
            return true;
        }
        else{
            //Time.timeScale = 0;
            life = 0;
            Debug.Log("GAME OVER");
            GameOverManager.GameOver();
            return false;
        }
    }

    public void IncreaseCurrency(int amount){
        currency += amount;
    } 

    public bool SpendCurrency(int amount){
        if(amount <= currency){
            //buy
            currency -= amount;
            return true;
        }
        else{
            Debug.Log("NO MONEY");
            return false;
        }
    }

    public void PauseGame(){
        Time.timeScale = 0f;
    }

    public void ResumeGame(){
        Time.timeScale = 1f;
    }



    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
