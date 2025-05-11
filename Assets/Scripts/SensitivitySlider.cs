using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SensitivitySlider : MonoBehaviour
{
    [SerializeField]
    private Slider sensSlider;
    void Start()
    {
        if(SettingsManager.instance != null)
        {
            sensSlider.value = SettingsManager.instance.Sensitivity;
            
        }
    }
    public void UpdateSens(float val)
    {
        if(SettingsManager.instance != null)
        {
            SettingsManager.instance.SetSensitivity(val);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
