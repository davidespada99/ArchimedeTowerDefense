using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour
{

    [Header("Attributes")]
    [SerializeField] private int hitPoints;
    [SerializeField] private int currencyWorth;

    private void Start(){
        hitPoints = (int)Math.Floor((float)hitPoints + Math.Log(LevelManager.main.GetCurrentWave()));  
    }

    public int GetHitPoints(){
        return hitPoints;
    }
    
    public void SetHitPoints(int _hitPoints){
        hitPoints = _hitPoints;
    }
    public void TakeDamage(int dmg){
        hitPoints -= dmg;

        if(hitPoints <= 0) {
            EnemySpawner.onEnemydestroy.Invoke();
            LevelManager.main.IncreaseCurrency(currencyWorth);  
            if(!EnemySpawner.GetisFirstEnemyKilled()){
                TutorialManager.instance.RunTutorialStep();
                EnemySpawner.SetisFirstEnemyKilled(true);
            }
            
            Destroy(gameObject);
        }
    }
}
