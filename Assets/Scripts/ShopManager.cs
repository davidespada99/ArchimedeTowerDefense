using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI currencyUI;
    [SerializeField] TextMeshProUGUI lifeUI;
    [SerializeField] TextMeshProUGUI WavesUI;
    
    void OnGUI(){
        currencyUI.text = LevelManager.main.currency.ToString();
        lifeUI.text = LevelManager.main.life.ToString();   
        WavesUI.text = LevelManager.main.waves.ToString();
    }
}
