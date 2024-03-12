using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemBalance : MonoBehaviour
{
    private int GemAmount;
    void Start()
    {   
        GemAmount = PlayerPrefs.GetInt("Gems");
    }

    public void AddGems()
    {
        GemAmount++;
        PlayerPrefs.SetInt("Gems", GemAmount);
    }
    public bool RemoveGems(int _Cost) 
    {
        if (GemAmount >= _Cost)
        {
            GemAmount -= _Cost;
            PlayerPrefs.SetInt("Gems", GemAmount);
            return true;
        }
        return false;
    }
}
