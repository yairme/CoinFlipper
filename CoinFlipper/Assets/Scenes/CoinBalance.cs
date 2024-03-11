using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using UnityEngine.UIElements;

public class CoinBalance : MonoBehaviour
{
    private long CoinAmount;


    void Start()
    {
        if (PlayerPrefs.HasKey("coins")) { CoinAmount = Convert.ToInt64(PlayerPrefs.GetString("coins")); }
    }
    private void FixedUpdate()
    {
        SaveValue();
    }
    private void ResetGame() 
    {
        CoinAmount = 0;
    }

    public void Swipe(long SwipeAmount)
    {
         CoinAmount += SwipeAmount;
    }
    public bool Buy(long price)
    {
        if(price <= CoinAmount)
        {
            CoinAmount -= price;
            return true;
        }
        return false;
    }
    private void SaveValue()
    {
        PlayerPrefs.SetString("Coins", CoinAmount.ToString());
    }
}
