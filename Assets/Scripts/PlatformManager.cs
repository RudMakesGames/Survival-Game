using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    [Header("Platform Settings")]
    [SerializeField] private GameObject LeftPlat, RightPlat, CentralPlat;
    private GameObject[] platforms;

    [Header("UI Settings")]
    [SerializeField] private TextMeshProUGUI WarningText;
    [SerializeField] private GameObject Warning;

    [Header("Timers")]
    [SerializeField] private float WarningDuration = 15f;
    [SerializeField] private float DisappearDuration = 60f;
    [SerializeField] private float InitialTimerThreshold = 60f;
    private float Timer;

    private bool StartDisappearingPlatforms = false;

    private void Start()
    {

        platforms = new GameObject[] { LeftPlat, RightPlat, CentralPlat };
       
    }

    private void Update()
    {
        Timer += Time.deltaTime;
        if (Timer >= InitialTimerThreshold && !StartDisappearingPlatforms)
        {
            StartDisappearingPlatforms = true;
            StartCoroutine(DisappearPlatformsSequence());
        }
    }

    private IEnumerator DisappearPlatformsSequence()
    {
        string[] platformNames = { "Left", "Right", "Center" };


        for (int i = 0; i < platforms.Length; i += 2)
        {

            int firstPlatformIndex = i;
            int secondPlatformIndex = (i + 1) % platforms.Length;


            WarningText.text = $"{platformNames[firstPlatformIndex]} and {platformNames[secondPlatformIndex]} Platforms will disappear in 15 seconds. Please move to other platforms.";
            WarningText.color = Color.red;
            Warning.SetActive(true);
            yield return new WaitForSeconds(WarningDuration);
            Warning.SetActive(false);


            platforms[firstPlatformIndex].SetActive(false);
           
            platforms[secondPlatformIndex].SetActive(false);
            
            yield return new WaitForSeconds(DisappearDuration);


            platforms[firstPlatformIndex].SetActive(true);
           

            platforms[secondPlatformIndex].SetActive(true);
            
        }


        StartDisappearingPlatforms = false;
        Timer = 0;


    }
}
