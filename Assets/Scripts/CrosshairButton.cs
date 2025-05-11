using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairButton : MonoBehaviour
{
    [SerializeField]
    private Image Crosshair;
    void Update()
    {
        
    }
    public void ConfirmCrosshair()
    {
        
            SettingsManager.instance.CrosshairImage = Crosshair.sprite;
            SettingsManager.instance.CrosshairColor = Crosshair.color;
            Debug.Log("Crosshair Changed to:" + Crosshair.sprite.name);

    }
}
