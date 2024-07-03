using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI currencyUI;
    
    void OnGUI(){
        currencyUI.text = LevelManager.main.currency.ToString();
    }
}
