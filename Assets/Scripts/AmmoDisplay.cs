using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmoDisplay : MonoBehaviour
{
    public TextMeshProUGUI AmmoText;
    public Weapons weapons;
    [SerializeField]
    bool isRifle, isSmg, isRevolver;
    void Start()
    {
        
    }

    // Update is called once per frame ling gang guli
    void Update()
    {
       if(isRifle)
        {
            AmmoText.text = weapons.RifleMag.ToString();
        }
       if(isSmg)
        {
            AmmoText.text = weapons.SmgMag.ToString();
        }
       if(isRevolver)
        {
            AmmoText.text = weapons.RevolverClip.ToString();
        }
    }
}
