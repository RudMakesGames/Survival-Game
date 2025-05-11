using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : Interactable
{
    public GameObject Door;
    public bool IsOpen = false;
    protected override void Interact()
    {
        Debug.Log("Interacted with" + gameObject.name);
        IsOpen = !IsOpen;
        Door.SetActive(IsOpen);

    }
}
