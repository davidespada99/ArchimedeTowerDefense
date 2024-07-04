using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour{

public static LevelManager main;


public Transform startPoint;
public Transform[] path;

[Header("Attributes")]
[SerializeField] public int currency = 50;
[SerializeField] public int life = 100;
[SerializeField] public int waves = 0;

    private void Awake(){
        main = this;
    }

    void Start(){
        currency = 50;
        life = 100;
        waves = 0;
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
            Debug.Log("GAME LOST");
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



    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
