using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    [Header("Attributes")]
    [SerializeField] private int hitPoints = 1;
    [SerializeField] private int currencyWorth = 1;


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
