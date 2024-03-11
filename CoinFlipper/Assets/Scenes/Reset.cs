using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Reset : MonoBehaviour
{
    public Action ResetGame;
    public Action<int> ShopMultiplayer;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ResetGame.Invoke();
        }
    }
}