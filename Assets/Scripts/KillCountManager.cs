using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.AI.Navigation;
using UnityEngine;

public class KillCountManager : MonoBehaviour
{
    public static KillCountManager instance;
    public int KillCount;
    [SerializeField]
    private TextMeshProUGUI KillCountText;
    [SerializeField]
    public int RequiredKills;
    private float currentTime;
    public bool isRunning = false;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    private void Start()
    {
      
    }
    private void Update()
    {
        
        UpdateUI();
    }
    void UpdateUI()
    {
         KillCountText.text =  KillCount.ToString(); 
    }
   
}

