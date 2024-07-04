using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    [Header("Attributes")]
    [SerializeField] private int hitPoints = 1;
    [SerializeField] private int currencyWorth = 1;

    private bool isDestroyed = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int dmg){
        hitPoints -= dmg;

        if(hitPoints <= 0) {
            EnemySpawner.onEnemydestroy.Invoke();
            LevelManager.main.IncreaseCurrency(currencyWorth);
            isDestroyed = true;

            Destroy(gameObject);
        }
    }
}
