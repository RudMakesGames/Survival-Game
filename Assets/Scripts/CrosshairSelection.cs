using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairSelection : MonoBehaviour
{
    
    private Image Crosshair;
    void Start()
    {
        Crosshair = GetComponent<Image>();
        if(SettingsManager.instance != null)
        {
            if(SettingsManager.instance.CrosshairImage!= null)
            {
                Crosshair.sprite = SettingsManager.instance.CrosshairImage;
                Crosshair.color = SettingsManager.instance.CrosshairColor;
            }
            
            else
            {
                Crosshair.sprite = SettingsManager.instance.DefaultCrosshair;
                Crosshair.color = Color.cyan;
            }
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
