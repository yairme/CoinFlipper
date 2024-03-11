using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemBalance : MonoBehaviour
{
    private int GemAmount;
    void Start()
    {   
        GemAmount = PlayerPrefs.GetInt("gems");
    }

    public void AddGems()
    {
        GemAmount++;
        PlayerPrefs.SetInt("gems", GemAmount);
    }
    public bool RemoveGems(int cost) 
    {
        if (GemAmount >= cost)
        {
            GemAmount -= cost;
            PlayerPrefs.SetInt("gems", GemAmount);
            return true;
        }
        return false;
    }
}
