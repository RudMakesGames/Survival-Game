using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PeriodicBlinking : MonoBehaviour
{
    [Header("Light Settings")]
    public Light[] lights; 
    [SerializeField]
    private float blinkInterval = 1f; 
    [SerializeField]
    private float blinkDuration = 0.5f; 

    private bool isBlinking = false;

    void Start()
    {
        if (lights.Length == 0)
        {
            lights = GetComponentsInChildren<Light>(); 
        }

       
        StartCoroutine(Blink());
    }

    IEnumerator Blink()
    {
        while (true)
        {
            foreach (var light in lights)
            {
                if (light != null)
                {
                    light.enabled = true; 
                }
            }

            yield return new WaitForSeconds(blinkDuration); 

            foreach (var light in lights)
            {
                if (light != null)
                {
                    light.enabled = false; 
                }
            }

            yield return new WaitForSeconds(blinkInterval - blinkDuration); 
        }
    }

    public void ToggleBlinking(bool enable)
    {
        if (enable && !isBlinking)
        {
            isBlinking = true;
            StartCoroutine(Blink());
        }
        else if (!enable && isBlinking)
        {
            isBlinking = false;
            StopAllCoroutines();
            foreach (var light in lights)
            {
                if (light != null)
                {
                    light.enabled = false; 
                }
            }
        }
    }

}
