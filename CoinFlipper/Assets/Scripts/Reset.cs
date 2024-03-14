using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Reset : MonoBehaviour
{
    public Action resetGame;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {resetGame.Invoke();} // link het aan een knop
    }
}