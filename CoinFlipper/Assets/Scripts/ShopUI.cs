using System.Collections;
using System.Collections.Generic;
    using UnityEngine;

    public class ShopUI : MonoBehaviour
    {
        public GameObject menuCanvas;

        public void ShowMenu()
        {
            menuCanvas.SetActive(true);
        }
        
        public void GoBack()
        {
            menuCanvas.SetActive(false);
        }
    }