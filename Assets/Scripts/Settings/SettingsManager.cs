using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
   public static SettingsManager instance;
    public float Sensitivity = 1000f;
    public Sprite CrosshairImage;
    public Color CrosshairColor;
    public Sprite DefaultCrosshair;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void SetSensitivity(float sens)
    {
        Sensitivity = sens;
        Debug.Log("Sensitivity updated to:" + sens);
    }
    
}
